using api_pos_biblioteca.Controllers;
using api_pos_reporte.Mediadores;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api_pos_reporte.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReportesController : CustomeControllerBase
    {
        private readonly IMediator _mediator;

        public ReportesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{tipoReporte}/{id}")]
        public async Task<IActionResult> ObtenerReportes(string tipoReporte, int id)
        {
            var resultado = await _mediator.Send(new ObtenerReporteRequest { TipoReporte = tipoReporte, Id = id });
            return RespuestaPerzonalizada(resultado);
        }
    }
}
