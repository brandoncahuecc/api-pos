using api_pos_biblioteca.Modelos;
using api_pos_biblioteca.Modelos.Global;
using api_pos_usuario.Persistencia;
using System.Security.Cryptography;
using System.Text;

namespace api_pos_usuario.Servicios
{
    public class UsuarioServicio : IUsuarioServicio
    {
        private readonly IUsuarioPersistencia _persistencia;

        public UsuarioServicio(IUsuarioPersistencia persistencia)
        {
            _persistencia = persistencia;
        }

        public async Task<Respuesta<Usuario, Mensaje>> IniciarSesion(string login, string clave)
        {
            var respuestaBusqueda = await _persistencia.BuscarUsuarioPorLogin(login);
            if (!respuestaBusqueda.Exito)
            {
                return respuestaBusqueda;
            }

            Usuario usuario = respuestaBusqueda.Objeto;

            Respuesta<Usuario, Mensaje> respuesta = new();
            //string claveDesencriptada = GetSha256Hash(usuario.clave);

            if (!clave.Equals(usuario.clave))
                return respuesta.RespuestaError(401, new Mensaje("NO-PASS-VALID", "Usuario o contraseña incorrecta"));

            GeneradorTokenJwt generador = new();

            string token = generador.GenerarTokenAcceso(usuario);

            usuario.Tokens = new Tokens()
            {
                Token = token,
                RefreshToken = "Pendiente"
            };

            return respuesta.RespuestaExito(usuario);
        }

        private string GetSha256Hash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(bytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }


        public async Task<Respuesta<Tokens, Mensaje>> RefrescarToken(Tokens tokens)
        {
            throw new NotImplementedException();
        }
    }

    public interface IUsuarioServicio
    {
        Task<Respuesta<Usuario, Mensaje>> IniciarSesion(string login, string clave);
        Task<Respuesta<Tokens, Mensaje>> RefrescarToken(Tokens tokens);
    }
}
