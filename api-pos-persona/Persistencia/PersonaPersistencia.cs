using api_pos_biblioteca.Modelos.Global;
using api_pos_biblioteca.Modelos;
using Dapper;
using MySql.Data.MySqlClient;

namespace api_pos_persona.Persistencia
{
    public class PersonaPersistencia : IPersonaPersistencia
    {
        private readonly string _stringConnection;

        public PersonaPersistencia()
        {
            _stringConnection = Environment.GetEnvironmentVariable("StringConnection") ?? string.Empty;
        }

        public async Task<Respuesta<Persona, Mensaje>> ActualizarPersona(Persona request)
        {
            using (MySqlConnection conn = new(_stringConnection))
            {
                var respuesta = new Respuesta<Persona, Mensaje>();
                Mensaje mensaje;
                try
                {
                    await conn.OpenAsync();

                    string query = @"UPDATE persona
SET nombre=@Nombre,
tipo_documento=@TipoDocumento,
num_documento=@NumDocumento,
direccion=@Direccion,
telefono=@Telefono,
email=@Email,
tipo_cliente=@TipoCliente
WHERE idpersona=@IdPersona;";

                    DynamicParameters parametros = new();
                    parametros.Add("@Nombre", request.Nombre);
                    parametros.Add("@TipoDocumento", request.TipoDocumento);
                    parametros.Add("@NumDocumento", request.NumDocumento);
                    parametros.Add("@Direccion", request.Direccion);
                    parametros.Add("@Telefono", request.Telefono);
                    parametros.Add("@Email", request.Email);
                    parametros.Add("@TipoCliente", request.TipoCliente);
                    parametros.Add("@IdPersona", request.IdPersona);

                    var resultado = await conn.ExecuteAsync(query, parametros);

                    if (resultado > 0)
                    {
                        return respuesta.RespuestaExito(request);
                    }

                    mensaje = new("NO-UPDATE-DB", "No fue posible actualizar la persona, vuelta a intentarlo o valide que exista");
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

        public async Task<Respuesta<Persona, Mensaje>> CrearPersona(Persona request)
        {
            using (MySqlConnection conn = new(_stringConnection))
            {
                var respuesta = new Respuesta<Persona, Mensaje>();
                Mensaje mensaje;
                try
                {
                    await conn.OpenAsync();

                    string query = @"INSERT INTO persona
(tipo_persona, nombre, tipo_documento, num_documento, direccion, telefono, email, tipo_cliente)
VALUES(@TipoPersona, @Nombre, @TipoDocumento, @NumDocumento, @Direccion, @Telefono, @Email, @TipoCliente);";

                    DynamicParameters parametros = new();
                    parametros.Add("@TipoPersona", request.TipoPersona);
                    parametros.Add("@Nombre", request.Nombre);
                    parametros.Add("@TipoDocumento", request.TipoDocumento);
                    parametros.Add("@NumDocumento", request.NumDocumento);
                    parametros.Add("@Direccion", request.Direccion);
                    parametros.Add("@Telefono", request.Telefono);
                    parametros.Add("@Email", request.Email);
                    parametros.Add("@TipoCliente", request.TipoCliente);

                    var resultado = await conn.ExecuteAsync(query, parametros);

                    if (resultado > 0)
                    {
                        var nuevoId = await conn.QueryFirstAsync<int>("SELECT LAST_INSERT_ID()");
                        request.IdPersona = nuevoId;
                        return respuesta.RespuestaExito(request);
                    }

                    mensaje = new("NO-CREATE-DB", "No fue posible crear la persona, revise los datos proporcionados y vuelta a intentarlo");
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

        public async Task<Respuesta<Mensaje, Mensaje>> EliminarPersona(int id)
        {
            Respuesta<Mensaje, Mensaje> respuesta = new();
            return respuesta.RespuestaError(501, new Mensaje("NO-IMPLEMENTED", "Esta acción no esta permitida"));

            //using (MySqlConnection conn = new(_stringConnection))
            //{
            //    Respuesta<Mensaje, Mensaje> respuesta = new();
            //    Mensaje mensaje;
            //    try
            //    {
            //        await conn.OpenAsync();

            //        string query = @"UPDATE categoria
            //    SET condicion = @Condicion
            //    WHERE idcategoria = @IdPersona";

            //        DynamicParameters parametros = new();
            //        parametros.Add("@Condicion", 0);
            //        parametros.Add("@IdPersona", id);

            //        var resultado = await conn.ExecuteAsync(query, parametros);

            //        if (resultado > 0)
            //        {
            //            mensaje = new("SUCCESS", "Categoría eliminada exitosamente");
            //            return respuesta.RespuestaExito(mensaje);
            //        }

            //        mensaje = new("NO-DELETE-DB", "Categoría no fue posible eliminarla, valide si existe dentro de los registros");
            //        return respuesta.RespuestaError(400, mensaje);
            //    }
            //    catch (Exception ex)
            //    {
            //        mensaje = new("NO-CON-DB", "Tenemos problemas con la base de datos, reporte al administrador", ex);
            //        return respuesta.RespuestaError(500, mensaje);
            //    }
            //    finally
            //    {
            //        if (conn is not null)
            //            await conn.CloseAsync();
            //    }
            //}
        }

        public async Task<Respuesta<Persona, Mensaje>> ObtenerPersona(int id)
        {
            using (MySqlConnection conn = new(_stringConnection))
            {
                Respuesta<Persona, Mensaje> respuesta = new();
                Mensaje mensaje;

                try
                {
                    await conn.OpenAsync();

                    string query = @"SELECT idpersona, tipo_persona as tipopersona, nombre,
tipo_documento as tipodocumento, num_documento as numdocumento,
direccion, telefono, email, tipo_cliente as tipocliente
FROM persona WHERE idpersona = @Id;";

                    var resultado = await conn.QueryFirstAsync<Persona>(query, new { Id = id });

                    if (resultado is not null)
                        return respuesta.RespuestaExito(resultado);

                    mensaje = new("NO-EXIST-DB", "Persona no se encuentra dentro de los registros");
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

        public async Task<Respuesta<List<Persona>, Mensaje>> ObtenerPersonas()
        {
            using (MySqlConnection conn = new(_stringConnection))
            {
                Respuesta<List<Persona>, Mensaje> respuesta = new();
                Mensaje mensaje;

                try
                {
                    await conn.OpenAsync();

                    string query = @"SELECT idpersona, tipo_persona as tipopersona, nombre,
tipo_documento as tipodocumento, num_documento as numdocumento,
direccion, telefono, email, tipo_cliente as tipocliente
FROM persona;";

                    var resultado = await conn.QueryAsync<Persona>(query);

                    if (resultado is not null)
                        return respuesta.RespuestaExito(resultado.ToList());

                    mensaje = new("NO-EXIST-DB", "No existen personas en la base de datos");
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

    public interface IPersonaPersistencia
    {
        Task<Respuesta<Persona, Mensaje>> ActualizarPersona(Persona request);
        Task<Respuesta<Persona, Mensaje>> CrearPersona(Persona request);
        Task<Respuesta<Mensaje, Mensaje>> EliminarPersona(int id);
        Task<Respuesta<Persona, Mensaje>> ObtenerPersona(int id);
        Task<Respuesta<List<Persona>, Mensaje>> ObtenerPersonas();
    }
}
