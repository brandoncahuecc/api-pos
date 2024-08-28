using api_pos_biblioteca.Modelos.Global;
using api_pos_persona.Servicios;
using MediatR;

namespace api_pos_persona.Mediadores
{
    public class EliminarPersonaRequest : IRequest<Respuesta<Mensaje, Mensaje>>
    {
        public int Id { get; set; }
    }

    public class EliminarPersonaHandler : IRequestHandler<EliminarPersonaRequest, Respuesta<Mensaje, Mensaje>>
    {
        private readonly IPersonaServicio _servicio;

        public EliminarPersonaHandler(IPersonaServicio servicio)
        {
            _servicio = servicio;
        }

        public async Task<Respuesta<Mensaje, Mensaje>> Handle(EliminarPersonaRequest request, CancellationToken cancellationToken)
        {
            var resultado = await _servicio.EliminarPersona(request.Id);
            return resultado;
        }
    }
}