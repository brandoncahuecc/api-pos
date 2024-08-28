using api_pos_biblioteca.Modelos.Global;
using api_pos_biblioteca.Modelos;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using api_pos_persona.Servicios;

namespace api_pos_persona.Mediadores
{
    public class ListarPersonaRequest : IRequest<Respuesta<List<Persona>, Mensaje>>
    {

    }

    public class ListarPersonaHandler : IRequestHandler<ListarPersonaRequest, Respuesta<List<Persona>, Mensaje>>
    {
        private readonly IPersonaServicio _servicio;

        public ListarPersonaHandler(IPersonaServicio servicio)
        {
            _servicio = servicio;
        }

        public async Task<Respuesta<List<Persona>, Mensaje>> Handle(ListarPersonaRequest request, CancellationToken cancellationToken)
        {
            Respuesta<List<Persona>, Mensaje> resultado = await _servicio.ObtenerPersonas();
            return resultado;
        }
    }
}