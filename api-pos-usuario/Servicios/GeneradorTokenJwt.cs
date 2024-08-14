using api_pos_biblioteca.Modelos;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;

namespace api_pos_usuario.Servicios
{
    public class GeneradorTokenJwt
    {
        public string GenerarTokenAcceso(Usuario usuario)
        {
            string claveSecreta = Environment.GetEnvironmentVariable("ClaveSecretaJwt") ?? string.Empty;
            string tiempoExpiracion = Environment.GetEnvironmentVariable("TiempoExpiracionJwt") ?? "1";
            var claveSecretaByte = Encoding.ASCII.GetBytes(claveSecreta);

            SecurityTokenDescriptor descriptor = new()
            {
                Subject = new ClaimsIdentity([
                    new Claim(ClaimTypes.Sid, usuario.IdUsuario.ToString()),
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim(ClaimTypes.Name, usuario.Nombre)]),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(tiempoExpiracion)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(claveSecretaByte), SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityTokenHandler handler = new();
            var token = handler.CreateToken(descriptor);
            return handler.WriteToken(token);
        }
    }
}
