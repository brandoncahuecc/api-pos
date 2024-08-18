using api_pos_biblioteca.Modelos;
using api_pos_biblioteca.Modelos.Global;
using api_pos_usuario.Persistencia;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
            var respuestaBusqueda = await _persistencia.BuscarUsuarioPorLogin(login, 0);
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
            string refreshToken = generador.GenerarRefreshToken();

            _persistencia.GuardarRefreshToken(usuario.IdUsuario, refreshToken);

            usuario.clave = string.Empty;

            usuario.Tokens = new Tokens()
            {
                Token = token,
                RefreshToken = refreshToken
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
            Respuesta<Tokens, Mensaje> respuesta = new();
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(tokens.Token);

            var userIdClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid);

            var idUsuario = Convert.ToInt32(userIdClaim.Value);

            _persistencia.ValidarRefreshToken(idUsuario, tokens.RefreshToken);

            var respuestaBusqueda = await _persistencia.BuscarUsuarioPorLogin(string.Empty, idUsuario);
            if (!respuestaBusqueda.Exito)
            {
                return respuesta.RespuestaError(401, respuestaBusqueda.Mensaje);
            }

            Usuario usuario = respuestaBusqueda.Objeto; 

            GeneradorTokenJwt generador = new();

            string token = generador.GenerarTokenAcceso(usuario);
            string refreshToken = generador.GenerarRefreshToken();

            _persistencia.GuardarRefreshToken(usuario.IdUsuario, refreshToken);

            usuario.clave = string.Empty;

            usuario.Tokens = new Tokens()
            {
                Token = token,
                RefreshToken = refreshToken
            };

            return respuesta.RespuestaExito(usuario.Tokens);
        }
    }

    public interface IUsuarioServicio
    {
        Task<Respuesta<Usuario, Mensaje>> IniciarSesion(string login, string clave);
        Task<Respuesta<Tokens, Mensaje>> RefrescarToken(Tokens tokens);
    }
}
