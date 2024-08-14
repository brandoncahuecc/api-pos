using api_pos_biblioteca.Controllers;
using api_pos_usuario.Mediadores;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace api_pos_usuario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : CustomeControllerBase
    {
        private readonly IMediator _mediator;

        public LoginController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> IniciarSesion(IniciarSesionRequest request)
        {
            var resultado = await _mediator.Send(request);
            return RespuestaPerzonalizada(resultado);
        }

        [HttpPost("Refresh")]
        public async Task<IActionResult> RefrescarToken(RefrescarTokenRequest request)
        {
            var resultado = await _mediator.Send(request);
            return RespuestaPerzonalizada(resultado);
        }
    }
}
