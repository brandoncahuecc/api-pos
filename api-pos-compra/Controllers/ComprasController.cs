using api_pos_biblioteca.Controllers;
using api_pos_compra.Mediadores;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api_pos_compra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ComprasController : CustomeControllerBase
    {
        private readonly IMediator _mediator;

        public ComprasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerCompras()
        {
            var resultado = await _mediator.Send(new ListarCompraRequest { });
            return RespuestaPerzonalizada(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> CrearCompra(CrearCompraRequest request)
        {
            var resultado = await _mediator.Send(request);
            return RespuestaPerzonalizada(resultado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarCompra(int id)
        {
            var resultado = await _mediator.Send(new EliminarCompraRequest { Id = id });
            return RespuestaPerzonalizada(resultado);
        }
    }
}
