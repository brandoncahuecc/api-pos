using api_pos_biblioteca.Controllers;
using api_pos_persona.Mediadores;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api_pos_persona.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class PersonasController : CustomeControllerBase
    {
        private readonly IMediator _mediator;

        public PersonasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerPersonas()
        {
            var resultado = await _mediator.Send(new ListarPersonaRequest { });
            return RespuestaPerzonalizada(resultado);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPersona(int id)
        {
            var resultado = await _mediator.Send(new ObtenerPersonaRequest { Id = id });
            return RespuestaPerzonalizada(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> CrearPersona(CrearPersonaRequest request)
        {
            var resultado = await _mediator.Send(request);
            return RespuestaPerzonalizada(resultado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarPersona(int id, ActualizarPersonaRequest request)
        {
            request.IdPersona = id;
            var resultado = await _mediator.Send(request);
            return RespuestaPerzonalizada(resultado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarPersona(int id)
        {
            var resultado = await _mediator.Send(new EliminarPersonaRequest { Id = id });
            return RespuestaPerzonalizada(resultado);
        }
    }
}
