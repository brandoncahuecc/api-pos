using api_pos_categoria.Mediadores.Categorias;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace api_pos_categoria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : CustomeControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerCategorias()
        {
            var resultado = await _mediator.Send(new ListarCategoriaRequest { });
            return RespuestaPerzonalizada(resultado);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerCategoria(int id)
        {
            var resultado = await _mediator.Send(new ObtenerCategoriaRequest { Id = id });
            return RespuestaPerzonalizada(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> CrearCategoria(CrearCategoriaRequest request)
        {
            var resultado = await _mediator.Send(request);
            return RespuestaPerzonalizada(resultado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarCategoria(int id, ActualizarCategoriaRequest request)
        {
            request.Id = id;
            var resultado = await _mediator.Send(request);
            return RespuestaPerzonalizada(resultado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarCategoria(int id)
        {
            var resultado = await _mediator.Send(new EliminarCategoriaRequest { Id = id });
            return RespuestaPerzonalizada(resultado);
        }
    }
}
