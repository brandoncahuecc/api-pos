using api_pos_biblioteca.Modelos.Global;
using api_pos_biblioteca.Modelos;
using MediatR;
using api_pos_persona.Servicios;

namespace api_pos_persona.Mediadores
{
    public class ObtenerPersonaRequest : IRequest<Respuesta<Persona, Mensaje>>
    {
        public int Id { get; set; }
    }

    public class ObtenerPersonaHandler : IRequestHandler<ObtenerPersonaRequest, Respuesta<Persona, Mensaje>>
    {
        private readonly IPersonaServicio _servicio;

        public ObtenerPersonaHandler(IPersonaServicio servicio)
        {
            _servicio = servicio;
        }

        public async Task<Respuesta<Persona, Mensaje>> Handle(ObtenerPersonaRequest request, CancellationToken cancellationToken)
        {
            var resultado = await _servicio.ObtenerPersona(request.Id);
            return resultado;
        }
    }
}