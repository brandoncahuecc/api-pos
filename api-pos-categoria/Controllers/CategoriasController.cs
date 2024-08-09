using api_pos_categoria.Mediadores.Categorias;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace api_pos_categoria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult ObtenerCategorias()
        {
            var resultado = _mediator.Send(new ListarCategoriaRequest { });
            return Ok(resultado.Result);
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerCategoria(int id)
        {
            var resultado = _mediator.Send(new ObtenerCategoriaRequest { Id = id });
            return Ok(resultado.Result);
        }

        [HttpPost]
        public IActionResult CrearCategoria(CrearCategoriaRequest request)
        {
            var resultado = _mediator.Send(request);
            return Ok(resultado.Result);
        }

        [HttpPut("{id}")]
        public IActionResult ActualizarCategoria(int id, ActualizarCategoriaRequest request)
        {
            request.Id = id;
            var resultado = _mediator.Send(request);
            return Ok(resultado.Result);
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarCategoria(int id)
        {
            var resultado = _mediator.Send(new EliminarCategoriaRequest { Id = id });
            return Ok(resultado.Result);
        }
    }
}
