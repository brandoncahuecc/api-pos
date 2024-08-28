using api_pos_biblioteca.Modelos.Global;
using api_pos_biblioteca.Modelos;
using MediatR;
using api_pos_persona.Servicios;

namespace api_pos_persona.Mediadores
{
    public class CrearPersonaRequest : IRequest<Respuesta<Persona, Mensaje>>
    {
        public string TipoPersona { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string TipoDocumento { get; set; } = string.Empty;
        public string NumDocumento { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string TipoCliente { get; set; } = string.Empty;
    }

    public class CrearPersonaHandler : IRequestHandler<CrearPersonaRequest, Respuesta<Persona, Mensaje>>
    {
        private readonly IPersonaServicio _servicio;

        public CrearPersonaHandler(IPersonaServicio servicio)
        {
            _servicio = servicio;
        }

        public async Task<Respuesta<Persona, Mensaje>> Handle(CrearPersonaRequest request, CancellationToken cancellationToken)
        {
            Persona persona = new()
            {
                TipoPersona = request.TipoPersona,
                Nombre = request.Nombre,
                TipoDocumento = request.TipoDocumento,
                NumDocumento = request.NumDocumento,
                Direccion = request.Direccion,
                Telefono = request.Telefono,
                Email = request.Email,
                TipoCliente = request.TipoCliente
            };

            var resultado = await _servicio.CrearPersona(persona);
            return resultado;
        }
    }
}
