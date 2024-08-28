using api_pos_biblioteca.Modelos.Global;
using api_pos_biblioteca.Modelos;
using MediatR;
using api_pos_persona.Servicios;

namespace api_pos_persona.Mediadores
{
    public class ActualizarPersonaRequest : IRequest<Respuesta<Persona, Mensaje>>
    {
        public int IdPersona { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string TipoDocumento { get; set; } = string.Empty;
        public string NumDocumento { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string TipoCliente { get; set; } = string.Empty;
    }

    public class ActualizarPersonaHandler : IRequestHandler<ActualizarPersonaRequest, Respuesta<Persona, Mensaje>>
    {
        private readonly IPersonaServicio _servicio;

        public ActualizarPersonaHandler(IPersonaServicio servicio)
        {
            _servicio = servicio;
        }

        public async Task<Respuesta<Persona, Mensaje>> Handle(ActualizarPersonaRequest request, CancellationToken cancellationToken)
        {
            Persona persona = new()
            {
                IdPersona = request.IdPersona,
                Nombre = request.Nombre,
                TipoDocumento = request.TipoDocumento,
                NumDocumento = request.NumDocumento,
                Direccion = request.Direccion,
                Telefono = request.Telefono,
                Email = request.Email,
                TipoCliente = request.TipoCliente
            };

            var resultado = await _servicio.ActualizarPersona(persona);
            return resultado;
        }
    }
}
