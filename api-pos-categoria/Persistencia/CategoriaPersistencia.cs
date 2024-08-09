using api_pos_categoria.Modelos;
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

    public async Task<Categoria> ActualizarCategoria(Categoria request)
    {
        using (MySqlConnection conn = new(_stringConnection))
        {
            Categoria categoria;
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
                    return request;
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (conn is not null)
                    await conn.CloseAsync();
            }
        }
    }

    public async Task<Categoria> CrearCategoria(Categoria request)
    {
        using (MySqlConnection conn = new(_stringConnection))
        {
            Categoria categoria;
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
                    return request;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (conn is not null)
                    await conn.CloseAsync();
            }
        }
    }

    public async Task<bool> EliminarCategoria(int id)
    {
        using (MySqlConnection conn = new(_stringConnection))
        {
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
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (conn is not null)
                    await conn.CloseAsync();
            }
        }
    }

    public async Task<Categoria> ObtenerCategoria(int id)
    {
        using (MySqlConnection conn = new(_stringConnection))
        {
            try
            {
                await conn.OpenAsync();

                var resultado = await conn.QueryFirstAsync<Categoria>("SELECT * FROM categoria WHERE condicion = 1 AND idcategoria = @Id", new { Id = id});

                return resultado;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (conn is not null)
                    await conn.CloseAsync();
            }
        }
    }

    public async Task<List<Categoria>> ObtenerCategorias()
    {
        using (MySqlConnection conn = new(_stringConnection))
        {
            try
            {
                await conn.OpenAsync();

                var resultado = await conn.QueryAsync<Categoria>("SELECT * FROM categoria WHERE condicion = 1");

                return resultado.ToList();
            }
            catch (Exception ex)
            {
                return null;
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
    Task<Categoria> ActualizarCategoria(Categoria request);
    Task<Categoria> CrearCategoria(Categoria request);
    Task<bool> EliminarCategoria(int id);
    Task<Categoria> ObtenerCategoria(int id);
    Task<List<Categoria>> ObtenerCategorias();
}