using api_pos_biblioteca.Modelos;
using api_pos_biblioteca.Modelos.Global;
using api_pos_reporte.Servicios;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace api_pos_reporte.Mediadores;

public class ObtenerReporteRequest : IRequest<Respuesta<Reporte, Mensaje>>
{
    public string TipoReporte { get; set; }
    public int Id { get; set; }
}

public class ObtenerReporteHandler : IRequestHandler<ObtenerReporteRequest, Respuesta<Reporte, Mensaje>>
{
    private readonly IReporteServicio _servicio;

    public ObtenerReporteHandler(IReporteServicio servicio)
    {
        _servicio = servicio;
    }

    public async Task<Respuesta<Reporte, Mensaje>> Handle(ObtenerReporteRequest request, CancellationToken cancellationToken)
    {
        Respuesta<Reporte, Mensaje> resultado = await _servicio.ObtenerReportes(request.TipoReporte, request.Id);
        return resultado;
    }
}