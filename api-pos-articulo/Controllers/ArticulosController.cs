using api_pos_articulo.Mediadores;
using api_pos_biblioteca.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_pos_articulo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ArticulosController : CustomeControllerBase
    {

        private readonly IMediator _mediator;

        public ArticulosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerArticulos()
        {
            var resultado = await _mediator.Send(new ListarArticulosRequest { });
            return RespuestaPerzonalizada(resultado);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerArticulo(int id)
        {
            var resultado = await _mediator.Send(new ObtenerArticuloRequest { Id = id });
            return RespuestaPerzonalizada(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> CrearArticulo(CrearArticuloRequest request)
        {
            var resultado = await _mediator.Send(request);
            return RespuestaPerzonalizada(resultado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarArticulo(int id/*, ActualizarCategoriaRequest request*/)
        {
            //    request.Id = id;
            //    var resultado = await _mediator.Send(request);
            //    return RespuestaPerzonalizada(resultado);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarArticulo(int id)
        {
            //var resultado = await _mediator.Send(new EliminarCategoriaRequest { Id = id });
            //return RespuestaPerzonalizada(resultado);
            return Ok();
        }
    }
}
