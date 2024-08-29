using api_pos_biblioteca.Modelos;
using api_pos_biblioteca.Modelos.Global;
using Dapper;
using MySql.Data.MySqlClient;

namespace api_pos_compra.Persistencia;

public class CompraPersistencia : ICompraPersistencia
{
    private readonly string _stringConnection;

    public CompraPersistencia()
    {
        _stringConnection = Environment.GetEnvironmentVariable("StringConnection") ?? string.Empty;
    }

    public async Task<Respuesta<Compra, Mensaje>> CrearCompra(Compra request)
    {
        using (MySqlConnection conn = new(_stringConnection))
        {
            var respuesta = new Respuesta<Compra, Mensaje>();
            Mensaje mensaje;
            try
            {
                await conn.OpenAsync();

                using (var transaccion = await conn.BeginTransactionAsync())
                {
                    try
                    {
                        string query = @"INSERT INTO ingreso
(idproveedor, idusuario, tipo_comprobante, serie_comprobante, num_comprobante, fecha_hora, impuesto, total_compra, estado)
VALUES(@IdProveedor, @IdUsuario, @TipoComprobante, @SerieComprobante, @NumComprobante, @FechaHora, @Impuesto, @TotalCompra, @Estado);";

                        DynamicParameters parametros = new();
                        parametros.Add("@IdProveedor", request.IdProveedor);
                        parametros.Add("@IdUsuario", request.IdUsuario);
                        parametros.Add("@TipoComprobante", request.TipoComprobante);
                        parametros.Add("@SerieComprobante", request.SerieComprobante);
                        parametros.Add("@NumComprobante", request.NumComprobante);
                        parametros.Add("@FechaHora", request.FechaHora);
                        parametros.Add("@Impuesto", request.Impuesto);
                        parametros.Add("@TotalCompra", request.TotalCompra);
                        parametros.Add("@Estado", "Aceptado");

                        var resultado = await conn.ExecuteAsync(query, parametros, transaccion);

                        if (resultado > 0)
                        {
                            var nuevoId = await conn.QueryFirstAsync<int>("SELECT LAST_INSERT_ID()");
                            request.IdIngreso = nuevoId;

                            //insertar el detalle
                            respuesta = await CrearDetalleCompra(request, conn, transaccion);
                            await transaccion.CommitAsync();
                            return respuesta;
                        }

                        mensaje = new("NO-CREATE-DB", "No fue posible crear la compra, revise los datos proporcionados y vuelta a intentarlo");
                        await transaccion.RollbackAsync();
                        return respuesta.RespuestaError(400, mensaje);
                    }
                    catch (Exception ex)
                    {
                        mensaje = new("NO-VALID-TRANSAC", "El almacenamiento de su compra no fue exitosa, reporte al administrador", ex);
                        await transaccion.RollbackAsync();
                        return respuesta.RespuestaError(500, mensaje);
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = new("NO-CON-DB", "Tenemos problemas con la base de datos, reporte al administrador", ex);
                return respuesta.RespuestaError(500, mensaje);
            }
            finally
            {
                if (conn is not null)
                    await conn.CloseAsync();
            }
        }
    }

    private async Task<Respuesta<Compra, Mensaje>> CrearDetalleCompra(Compra request, MySqlConnection conn, MySqlTransaction transaccion)
    {
        request.Detalle.ForEach(async item =>
        {
            string query = @"INSERT INTO detalle_ingreso
(idingreso, idarticulo, cantidad, precio_compra, precio_venta, stock)
VALUES(@IdIngreso, @IdArticulo, @Cantidad, @PrecioCompra, @PrecioVenta, @Stock);";

            DynamicParameters parametros = new();
            parametros.Add("@IdIngreso", request.IdIngreso);
            parametros.Add("@IdArticulo", item.IdArticulo);
            parametros.Add("@Cantidad", item.Cantidad);
            parametros.Add("@PrecioCompra", item.PrecioCompra);
            parametros.Add("@PrecioVenta", item.PrecioVenta);
            parametros.Add("@Stock", item.Stock);

            var resultado = await conn.ExecuteAsync(query, parametros, transaccion);

            if (resultado > 0)
            {
                var nuevoId = await conn.QueryFirstAsync<int>("SELECT LAST_INSERT_ID()");
                item.IdDetalleIngreso = nuevoId;
            }
            else
            {
                throw new Exception("No fue posible insertar el detalle de la compra, revise los datos proporcionados y vuelta a intentarlo");
            }
        });

        Respuesta<Compra, Mensaje> respuesta = new();
        return respuesta.RespuestaExito(request);
    }

    public async Task<Respuesta<Mensaje, Mensaje>> EliminarCompra(int id)
    {
        using (MySqlConnection conn = new(_stringConnection))
        {
            Respuesta<Mensaje, Mensaje> respuesta = new();
            Mensaje mensaje;
            try
            {
                await conn.OpenAsync();

                string query = @"UPDATE ingreso
SET estado = @Estado WHERE idingreso = @IdCompra;";

                DynamicParameters parametros = new();
                parametros.Add("@Estado", "Anulado");
                parametros.Add("@IdCompra", id);

                var resultado = await conn.ExecuteAsync(query, parametros);

                if (resultado > 0)
                {
                    mensaje = new("SUCCESS", "Compra eliminada exitosamente");
                    return respuesta.RespuestaExito(mensaje);
                }

                mensaje = new("NO-DELETE-DB", "Compra no fue posible eliminarla, valide si existe dentro de los registros");
                return respuesta.RespuestaError(400, mensaje);
            }
            catch (Exception ex)
            {
                mensaje = new("NO-CON-DB", "Tenemos problemas con la base de datos, reporte al administrador", ex);
                return respuesta.RespuestaError(500, mensaje);
            }
            finally
            {
                if (conn is not null)
                    await conn.CloseAsync();
            }
        }
    }

    public async Task<Respuesta<List<Compra>, Mensaje>> ObtenerCompras()
    {
        using (MySqlConnection conn = new(_stringConnection))
        {
            Respuesta<List<Compra>, Mensaje> respuesta = new();
            Mensaje mensaje;

            try
            {
                await conn.OpenAsync();

                string query = @"SELECT idingreso, idproveedor, idusuario, idusuario_update as idusuarioupdate,
tipo_comprobante as tipocomprobante, serie_comprobante as seriecomprobante, num_comprobante as numcomprobante,
fecha_hora as fechahora, impuesto, total_compra as totalcompra, estado, forma_pago as formapago,
dias_credito as diascredito, fecha_hora_pago_credito as fechahorapagocredito, valor_pagar as valorpagar,
saldo_ingreso as saldoingreso, tipo_pago as tipopago, no_cheque as nocheque,
NULLIF(fecha_hora_generacion_pago, '0000-00-00 00:00:00') as fechahorageneracionpago,
tipo_banco as tipobanco, numero_boleta as numeroboleta, recibo_caja_numero as recibocajanumero,
direccion_entrega_orden_compra as direccionentregaordencompra, fecha_entrega_orden_compra as fechaentregaordencompra,
observacion_orden_compra as observacionordencompra, usuarui_modificacion as usuaruimodificacion,
NULLIF(fecha_modificacion, '0000-00-00 00:00:00') as fechamodificacion,
motivo_modificacion as motivomodificacion, num_correlativo as numcorrelativo
FROM ingreso;";

                var resultado = await conn.QueryAsync<Compra>(query);

                if (resultado is not null)
                    return respuesta.RespuestaExito(resultado.ToList());

                mensaje = new("NO-EXIST-DB", "No existen comrpas en la base de datos");
                return respuesta.RespuestaError(400, mensaje);
            }
            catch (Exception ex)
            {
                mensaje = new("NO-CON-DB", "Tenemos problemas con la base de datos, reporte al administrador", ex);
                return respuesta.RespuestaError(500, mensaje);
            }
            finally
            {
                if (conn is not null)
                    await conn.CloseAsync();
            }
        }
    }
}

public interface ICompraPersistencia
{
    Task<Respuesta<Compra, Mensaje>> CrearCompra(Compra request);
    Task<Respuesta<Mensaje, Mensaje>> EliminarCompra(int id);
    Task<Respuesta<List<Compra>, Mensaje>> ObtenerCompras();
}