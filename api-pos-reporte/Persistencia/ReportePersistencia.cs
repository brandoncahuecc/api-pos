using api_pos_biblioteca.Modelos;
using api_pos_biblioteca.Modelos.Global;
using Dapper;
using MySql.Data.MySqlClient;

namespace api_pos_reporte.Persistencia;

public class ReportePersistencia : IReportePersistencia
{
    private readonly string _stringConnection;

    public ReportePersistencia()
    {
        _stringConnection = Environment.GetEnvironmentVariable("StringConnection") ?? string.Empty;
    }

    public async Task<Respuesta<Compra, Mensaje>> ObtenerCompraReportes(int id)
    {
        using (MySqlConnection conn = new(_stringConnection))
        {
            Respuesta<Compra, Mensaje> respuesta = new();
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
FROM ingreso WHERE idingreso = @IdIngreso;";

                string queryDetalle = @"SELECT iddetalle_ingreso as iddetalleingreso,
idingreso, idarticulo, cantidad,
precio_compra as preciocompra, precio_venta as precioventa, stock
FROM detalle_ingreso WHERE idingreso = @IdIngreso;
";

                var resultado = await conn.QueryFirstAsync<Compra>(query, new { IdIngreso = id });

                var resultadoDetalle = await conn.QueryAsync<DetalleIngreso>(queryDetalle, new { IdIngreso = id });

                if (resultado is not null && resultadoDetalle is not null)
                {
                    resultado.Detalle = resultadoDetalle.ToList();
                    return respuesta.RespuestaExito(resultado);
                }

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

public interface IReportePersistencia
{
    Task<Respuesta<Compra, Mensaje>> ObtenerCompraReportes(int id);
}