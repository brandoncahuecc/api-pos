using System;
using api_pos_biblioteca.Modelos;
using api_pos_biblioteca.Modelos.Global;
using api_pos_reporte.Persistencia;
using api_pos_reporte.Recursos;

namespace api_pos_reporte.Servicios;

public class ReporteServicio : IReporteServicio
{
    private readonly IReportePersistencia _persistencia;

    public ReporteServicio(IReportePersistencia persistencia)
    {
        _persistencia = persistencia;
    }

    public async Task<Respuesta<Reporte, Mensaje>> ObtenerReportes(string tipoReporte, int id)
    {

        Respuesta<Reporte, Mensaje> respuesta = new();
        switch (tipoReporte)
        {
            case "Compra":

                var resultado = await _persistencia.ObtenerCompraReportes(id);

                if (resultado.Exito)
                {
                    Compra compra = resultado.Objeto;

                    string html = Plantillas.HtmlCompra;
                    string htmlBaseDetalle = Plantillas.HtmlCompraDetalle;

                    string htmlDetalle = string.Empty;

                    compra.Detalle.ForEach(item =>
                    {
                        htmlDetalle += htmlBaseDetalle
                        .Replace("@IdIngreso", item.IdIngreso.ToString())
                        .Replace("@IdArticulo", item.IdArticulo.ToString())
                        .Replace("@Cantidad", item.Cantidad.ToString())
                        .Replace("@PrecioCompra", item.PrecioCompra.ToString("0.00"))
                        .Replace("@PrecioVenta", item.PrecioVenta.ToString("0.00"))
                        .Replace("@Stock", item.Stock.ToString("0.00"));
                    });

                    html = html
                        .Replace("@FechaReporte", DateTime.Now.ToString("dd/MM/yyyy HH:mm"))
                        .Replace("@TotalIngresos", compra.TotalCompra is null ? "0.00" : compra.TotalCompra.Value.ToString("0.00"))
                        .Replace("@DetalleArticulos", htmlDetalle);

                    return respuesta.RespuestaExito(new Reporte() { Formato = "HTML", Base64 = html });
                }

                return respuesta.RespuestaError(400, resultado.Mensaje);
            default:
                return respuesta.RespuestaError(501, new("NOT-IMPLEM", $"El reporte {tipoReporte} no esta implementado"));
        }
    }
}

public interface IReporteServicio
{
    Task<Respuesta<Reporte, Mensaje>> ObtenerReportes(string tipoReporte, int id);
}