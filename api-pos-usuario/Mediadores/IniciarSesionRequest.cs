using api_pos_biblioteca.Modelos;
using api_pos_biblioteca.Modelos.Global;
using api_pos_usuario.Servicios;
using MediatR;

namespace api_pos_usuario.Mediadores
{
    public class IniciarSesionRequest : IRequest<Respuesta<Usuario, Mensaje>>
    {
        public string Usuario { get; set; } = string.Empty;
        public string Clave { get; set; } = string.Empty;
    }

    public class IniciarSesionHandler : IRequestHandler<IniciarSesionRequest, Respuesta<Usuario, Mensaje>>
    {
        private readonly IUsuarioServicio _servicio;

        public IniciarSesionHandler(IUsuarioServicio servicio)
        {
            _servicio = servicio;
        }

        public async Task<Respuesta<Usuario, Mensaje>> Handle(IniciarSesionRequest request, CancellationToken cancellationToken)
        {
            var resultado = await _servicio.IniciarSesion(request.Usuario, request.Clave);
            return resultado;
        }
    }
}
