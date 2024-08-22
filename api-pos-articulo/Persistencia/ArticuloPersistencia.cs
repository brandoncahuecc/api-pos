using api_pos_biblioteca.Modelos.Global;
using api_pos_biblioteca.Modelos;
using MySql.Data.MySqlClient;
using Dapper;

namespace api_pos_articulo.Persistencia
{
    public class ArticuloPersistencia : IArticuloPersistencia
    {
        private readonly string _stringConnection;

        public ArticuloPersistencia()
        {
            _stringConnection = Environment.GetEnvironmentVariable("StringConnection") ?? string.Empty;
        }

        public async Task<Respuesta<Articulo, Mensaje>> ActualizarArticulo(Articulo request)
        {
            throw new NotImplementedException();
        }

        public async Task<Respuesta<Articulo, Mensaje>> CrearArticulo(Articulo request)
        {
            using (MySqlConnection conn = new(_stringConnection))
            {
                var respuesta = new Respuesta<Articulo, Mensaje>();
                Mensaje mensaje;
                try
                {
                    await conn.OpenAsync();

                    string query = @"INSERT INTO articulo
(idcategoria, idmarca, idlinea, codigo, nombre, stock,
stockminimo, descripcion, imagen, condicion,
precio_venta, idusuario, precio_compra, fecha_add)
VALUES(@IdCategoria, 0, 0, @Codigo, @Nombre, @Stock,
@StockMinimo, @Descripcion, @Imagen, 1, @PrecioVenta,
@IdUsuario, @PrecioCompra, @FechaAdd);";

                    DynamicParameters parametros = new();
                    parametros.Add("@IdCategoria", request.IdCategoria);
                    parametros.Add("@Codigo", request.Codigo);
                    parametros.Add("@Nombre", request.Nombre);
                    parametros.Add("@Stock", request.Stock);
                    parametros.Add("@StockMinimo", request.StockMinimo);
                    parametros.Add("@Descripcion", request.Descripcion);
                    parametros.Add("@Imagen", request.Imagen);
                    parametros.Add("@PrecioVenta", request.PrecioVenta);
                    parametros.Add("@IdUsuario", request.IdUsuario);
                    parametros.Add("@PrecioCompra", request.PrecioCompra);
                    parametros.Add("@FechaAdd", request.FechaAdd);

                    var resultado = await conn.ExecuteAsync(query, parametros);

                    if (resultado > 0)
                    {
                        var nuevoId = await conn.QueryFirstAsync<int>("SELECT LAST_INSERT_ID()");
                        request.IdArticulo = nuevoId;
                        return respuesta.RespuestaExito(request);
                    }

                    mensaje = new("NO-CREATE-DB", "No fue posible crear el artículo, revise los datos proporcionados y vuelta a intentarlo");
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

        public async Task<Respuesta<Mensaje, Mensaje>> EliminarArticulo(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Respuesta<Articulo, Mensaje>> ObtenerArticulo(int id)
        {
            using (MySqlConnection conn = new(_stringConnection))
            {
                Respuesta<Articulo, Mensaje> respuesta = new();
                Mensaje mensaje;

                try
                {
                    await conn.OpenAsync();

                    string query = @"SELECT idarticulo, idcategoria,
idmarca, idlinea, codigo,
nombre, ano_de as anode, ano_a as anoa,
stock, stockminimo,
descripcion, imagen,
condicion, precio_venta as precioventa,
descuento_porcentaje as descuentoporcentaje,
precio_descuento as preciodescuento,
pocerntaje_precio_mayorista as pocerntajepreciomayorista,
precio_mayorista as preciomayorista,
pocerntaje_precio_minorista as pocerntajepreciominorista,
precio_minorista as preciominorista,
pocerntaje_precio_menudeo as pocerntajepreciomenudeo,
precio_menudeo as preciomenudeo, peso_producto as pesoproducto,
tipo_producto as tipoproducto, idusuario,
idusuario_update as idusuarioupdate, precio_compra as preciocompra,
editorial_marca as editorialmarca, autor, yaer,
paginas, Edicion, cod_inter as codinter,
tipo_estado_libro as tipoestadolibro, tipo_consignacion as tipoconsignacion,
ubicacion, fecha_add as fechaadd, fecha_update as fechaupdate
FROM articulo WHERE condicion = 1 AND idarticulo = @Id;";

                    var resultado = await conn.QueryFirstAsync<Articulo>(query, new {Id = id});

                    if (resultado is not null)
                        return respuesta.RespuestaExito(resultado);

                    mensaje = new("NO-EXIST-DB", "Artículo no se encuentra dentro de los registros");
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

        public async Task<Respuesta<List<Articulo>, Mensaje>> ObtenerArticulos()
        {
            using (MySqlConnection conn = new(_stringConnection))
            {
                Respuesta<List<Articulo>, Mensaje> respuesta = new();
                Mensaje mensaje;

                try
                {
                    await conn.OpenAsync();

                    var resultado = await conn.QueryAsync<Articulo>(@"SELECT idarticulo, idcategoria,
idmarca, idlinea, codigo,
nombre, ano_de as anode, ano_a as anoa,
stock, stockminimo,
descripcion, imagen,
condicion, precio_venta as precioventa,
descuento_porcentaje as descuentoporcentaje,
precio_descuento as preciodescuento,
pocerntaje_precio_mayorista as pocerntajepreciomayorista,
precio_mayorista as preciomayorista,
pocerntaje_precio_minorista as pocerntajepreciominorista,
precio_minorista as preciominorista,
pocerntaje_precio_menudeo as pocerntajepreciomenudeo,
precio_menudeo as preciomenudeo, peso_producto as pesoproducto,
tipo_producto as tipoproducto, idusuario,
idusuario_update as idusuarioupdate, precio_compra as preciocompra,
editorial_marca as editorialmarca, autor, yaer,
paginas, Edicion, cod_inter as codinter,
tipo_estado_libro as tipoestadolibro, tipo_consignacion as tipoconsignacion,
ubicacion, fecha_add as fechaadd, fecha_update as fechaupdate
FROM articulo WHERE condicion = 1;");

                    if (resultado is not null)
                        return respuesta.RespuestaExito(resultado.ToList());

                    mensaje = new("NO-EXIST-DB", "No existen artículos en la base de datos");
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

    public interface IArticuloPersistencia
    {
        Task<Respuesta<Articulo, Mensaje>> ActualizarArticulo(Articulo request);
        Task<Respuesta<Articulo, Mensaje>> CrearArticulo(Articulo request);
        Task<Respuesta<Mensaje, Mensaje>> EliminarArticulo(int id);
        Task<Respuesta<Articulo, Mensaje>> ObtenerArticulo(int id);
        Task<Respuesta<List<Articulo>, Mensaje>> ObtenerArticulos();
    }
}
