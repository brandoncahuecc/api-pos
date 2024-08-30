using System;
using api_pos_biblioteca.Modelos;
using api_pos_biblioteca.Modelos.Global;
using api_pos_reporte.Persistencia;
using api_pos_reporte.Recursos;
using DinkToPdf;
using DinkToPdf.Contracts;

namespace api_pos_reporte.Servicios;

public class ReporteServicio : IReporteServicio
{
    private readonly IReportePersistencia _persistencia;
    private readonly IConverter _converter;

    public ReporteServicio(IReportePersistencia persistencia, IConverter converter)
    {
        _persistencia = persistencia;
        _converter = converter;
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

                    string img = await ObtenerBase64Imagen(Plantillas.UrlLogo);

                    html = html
                        .Replace("@FechaReporte", DateTime.Now.ToString("dd/MM/yyyy HH:mm"))
                        .Replace("@TotalIngresos", compra.TotalCompra is null ? "0.00" : compra.TotalCompra.Value.ToString("0.00"))
                        .Replace("@DetalleArticulos", htmlDetalle)
                        .Replace("@LogoImg", img);

                    string base64 = GenerarPdf(html);
                    
                    return respuesta.RespuestaExito(new Reporte() { Nombre = $"Compra-{id}-{DateTime.Now.ToString("ddMMyyyy-HHmm")}", Formato = "application/pdf", Base64 = base64 });
                }

                return respuesta.RespuestaError(400, resultado.Mensaje);
            default:
                return respuesta.RespuestaError(501, new("NOT-IMPLEM", $"El reporte {tipoReporte} no esta implementado"));
        }
    }

    private string GenerarPdf(string html)
    {
        var pdf = new HtmlToPdfDocument
        {
            GlobalSettings = new GlobalSettings
            {
                PaperSize = PaperKind.A2,
                Orientation = Orientation.Portrait
            },
            Objects =
            {
                new ObjectSettings
                {
                    HtmlContent = html,
                    WebSettings =
                    {
                        DefaultEncoding = "utf-8",
                        LoadImages = true,
                        PrintMediaType = true,
                        EnableIntelligentShrinking = false
                    },
                    UseExternalLinks = true,
                    UseLocalLinks = true
                }
            }
        };

        var document = _converter.Convert(pdf);
        return Convert.ToBase64String(document);
    }

    private async Task<string> ObtenerBase64Imagen(string url)
    {
        try
        {
            using (HttpClient client = new HttpClient())
            {
                byte[] imageBytes = await client.GetByteArrayAsync(url);
                string base64String = Convert.ToBase64String(imageBytes);
                return $"data:image/jpeg;base64,{base64String}";
            }
        }
        catch
        {
            return string.Empty;
        }
    }
}

public interface IReporteServicio
{
    Task<Respuesta<Reporte, Mensaje>> ObtenerReportes(string tipoReporte, int id);
}