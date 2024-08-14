using api_pos_biblioteca.Modelos;
using api_pos_biblioteca.Modelos.Global;
using api_pos_usuario.Servicios;
using MediatR;

namespace api_pos_usuario.Mediadores
{
    public class RefrescarTokenRequest : IRequest<Respuesta<Tokens, Mensaje>>
    {
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }

    public class RefrescarTokenHandler : IRequestHandler<RefrescarTokenRequest, Respuesta<Tokens, Mensaje>>
    {
        private readonly IUsuarioServicio _servicio;

        public RefrescarTokenHandler(IUsuarioServicio servicio)
        {
            _servicio = servicio;
        }

        public async Task<Respuesta<Tokens, Mensaje>> Handle(RefrescarTokenRequest request, CancellationToken cancellationToken)
        {
            Tokens token = new()
            {
                Token = request.Token,
                RefreshToken = request.RefreshToken
            };

            var resultado = await _servicio.RefrescarToken(token);
            return resultado;
        }
    }
}
