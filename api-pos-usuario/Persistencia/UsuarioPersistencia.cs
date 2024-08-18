using api_pos_biblioteca.Modelos;
using api_pos_biblioteca.Modelos.Global;
using Dapper;
using MySql.Data.MySqlClient;

namespace api_pos_usuario.Persistencia
{
    public class UsuarioPersistencia : IUsuarioPersistencia
    {
        private readonly string _stringConnection;

        public UsuarioPersistencia()
        {
            _stringConnection = Environment.GetEnvironmentVariable("StringConnection") ?? string.Empty;
        }

        public async Task<Respuesta<Usuario, Mensaje>> BuscarUsuarioPorLogin(string login, int idUsuario)
        {
            using (MySqlConnection conn = new(_stringConnection))
            {
                Respuesta<Usuario, Mensaje> respuesta = new();
                Mensaje mensaje;

                try
                {
                    await conn.OpenAsync();

                    string query = @"SELECT
idusuario,
nombre,
tipo_documento as tipodocumento,
num_documento as numdocumento,
direccion,
telefono,
email,
cargo,
login,
clave,
imagen,
idsucursal,
condicion
FROM usuario
WHERE condicion = 1
AND (login = @Login OR idusuario = @IdUsuario)";

                    var resultado = await conn.QueryFirstAsync<Usuario>(query, new { Login = login, IdUsuario = idUsuario });

                    if (resultado is not null)
                        return respuesta.RespuestaExito(resultado);

                    mensaje = new("NO-EXIST-DB", "Usuario o contraseña incorrecta");
                    return respuesta.RespuestaError(401, mensaje);
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

        public Respuesta<Mensaje, Mensaje> GuardarRefreshToken(int idUsuario, string refreshToken)
        {
            Respuesta<Mensaje, Mensaje> respuesta = new();
            /*
             Todo el codigo necesario para almacenar el token en base de datos relacionado al Usuario
             */


            return respuesta.RespuestaExito(new Mensaje("SUCCESS", "Token almacenado"));
        }

        public Respuesta<Mensaje, Mensaje> ValidarRefreshToken(int idUsuario, string refreshToken)
        {
            Respuesta<Mensaje, Mensaje> respuesta = new();
            /*
            Todo el codigo necesario para validar que el token en base de datos relacionado al Usuario aun este vigente y activo
            */

            return respuesta.RespuestaExito(new Mensaje("SUCCESS", "Token Valido"));
        }
    }

    public interface IUsuarioPersistencia
    {
        Task<Respuesta<Usuario, Mensaje>> BuscarUsuarioPorLogin(string login, int idUsuario);
        Respuesta<Mensaje, Mensaje> GuardarRefreshToken(int idUsuario, string refreshToken);

        Respuesta<Mensaje, Mensaje> ValidarRefreshToken(int idUsuario, string refreshToken);
    }
}
