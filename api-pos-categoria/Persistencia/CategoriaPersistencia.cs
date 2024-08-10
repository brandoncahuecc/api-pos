using api_pos_categoria.Modelos;
using api_pos_categoria.Modelos.Global;
using Dapper;
using MySql.Data.MySqlClient;

namespace api_pos_categoria.Persistencia;

public class CategoriaPersistencia : ICategoriaPersistencia
{
    private readonly string _stringConnection;

    public CategoriaPersistencia()
    {
        _stringConnection = Environment.GetEnvironmentVariable("StringConnection") ?? string.Empty;
    }

    public async Task<Respuesta<Categoria, Mensaje>> ActualizarCategoria(Categoria request)
    {
        using (MySqlConnection conn = new(_stringConnection))
        {
            var respuesta = new Respuesta<Categoria, Mensaje>();
            Mensaje mensaje;
            try
            {
                await conn.OpenAsync();

                string query = @"UPDATE categoria
                SET nombre = @Nombre,
                descripcion = @Descripcion
                WHERE idcategoria = @IdCategoria";

                DynamicParameters parametros = new();
                parametros.Add("@Nombre", request.Nombre);
                parametros.Add("@Descripcion", request.Descripcion);
                parametros.Add("@IdCategoria", request.IdCategoria);

                var resultado = await conn.ExecuteAsync(query, parametros);

                if (resultado > 0)
                {
                    return respuesta.RespuestaExito(request);
                }

                mensaje = new("NO-UPDATE-DB", "No fue posible actualizar la categoria, vuelta a intentarlo o valide que exista");
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

    public async Task<Respuesta<Categoria, Mensaje>> CrearCategoria(Categoria request)
    {
        using (MySqlConnection conn = new(_stringConnection))
        {
            var respuesta = new Respuesta<Categoria, Mensaje>();
            Mensaje mensaje;
            try
            {
                await conn.OpenAsync();

                string query = @"INSERT INTO categoria
                (nombre, descripcion, condicion)
                VALUES (@Nombre, @Descripcion, @Condicion)";

                DynamicParameters parametros = new();
                parametros.Add("@Nombre", request.Nombre);
                parametros.Add("@Descripcion", request.Descripcion);
                parametros.Add("@Condicion", 1);

                var resultado = await conn.ExecuteAsync(query, parametros);

                if (resultado > 0)
                {
                    var nuevoId = await conn.QueryFirstAsync<int>("SELECT LAST_INSERT_ID()");
                    request.IdCategoria = nuevoId;
                    return respuesta.RespuestaExito(request);
                }

                mensaje = new("NO-CREATE-DB", "No fue posible crear la categoria, revise los datos proporcionados y vuelta a intentarlo");
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

    public async Task<Respuesta<Mensaje, Mensaje>> EliminarCategoria(int id)
    {
        using (MySqlConnection conn = new(_stringConnection))
        {
            Respuesta<Mensaje, Mensaje> respuesta = new();
            Mensaje mensaje;
            try
            {
                await conn.OpenAsync();

                string query = @"UPDATE categoria
                SET condicion = @Condicion
                WHERE idcategoria = @IdCategoria";

                DynamicParameters parametros = new();
                parametros.Add("@Condicion", 0);
                parametros.Add("@IdCategoria", id);

                var resultado = await conn.ExecuteAsync(query, parametros);

                if (resultado > 0)
                {
                    mensaje = new("SUCCESS", "Categoría eliminada exitosamente");
                    return respuesta.RespuestaExito(mensaje);
                }

                mensaje = new("NO-DELETE-DB", "Categoría no fue posible eliminarla, valide si existe dentro de los registros");
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

    public async Task<Respuesta<Categoria, Mensaje>> ObtenerCategoria(int id)
    {
        using (MySqlConnection conn = new(_stringConnection))
        {
            Respuesta<Categoria, Mensaje> respuesta = new();
            Mensaje mensaje;

            try
            {
                await conn.OpenAsync();

                var resultado = await conn.QueryFirstAsync<Categoria>("SELECT * FROM categoria WHERE condicion = 1 AND idcategoria = @Id", new { Id = id });

                if (resultado is not null)
                    return respuesta.RespuestaExito(resultado);

                mensaje = new("NO-EXIST-DB", "Categoría no se encuentra dentro de los registros");
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

    public async Task<Respuesta<List<Categoria>, Mensaje>> ObtenerCategorias()
    {
        using (MySqlConnection conn = new(_stringConnection))
        {
            Respuesta<List<Categoria>, Mensaje> respuesta = new();
            Mensaje mensaje;

            try
            {
                await conn.OpenAsync();

                var resultado = await conn.QueryAsync<Categoria>("SELECT * FROM categoria WHERE condicion = 1");

                if (resultado is not null)
                    return respuesta.RespuestaExito(resultado.ToList());

                mensaje = new("NO-EXIST-DB", "No existen categorías en la base de datos");
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

public interface ICategoriaPersistencia
{
    Task<Respuesta<Categoria, Mensaje>> ActualizarCategoria(Categoria request);
    Task<Respuesta<Categoria, Mensaje>> CrearCategoria(Categoria request);
    Task<Respuesta<Mensaje, Mensaje>> EliminarCategoria(int id);
    Task<Respuesta<Categoria, Mensaje>> ObtenerCategoria(int id);
    Task<Respuesta<List<Categoria>, Mensaje>> ObtenerCategorias();
}