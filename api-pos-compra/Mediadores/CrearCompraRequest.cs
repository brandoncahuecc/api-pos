using api_pos_biblioteca.Modelos;
using api_pos_biblioteca.Modelos.Global;
using api_pos_compra.Servicios;
using MediatR;
using System.Security.Claims;

namespace api_pos_compra.Mediadores;

public class CrearCompraRequest : IRequest<Respuesta<Compra, Mensaje>>
{
    public int IdProveedor { get; set; }
    public string TipoComprobante { get; set; }
    public string SerieComprobante { get; set; }
    public string NumComprobante { get; set; }
    public Decimal Impuesto { get; set; }
    public Decimal TotalCompra { get; set; }
    public List<DetalleIngresoRequest> Detalle { get; set; }
}

public class DetalleIngresoRequest
{
    public int IdArticulo { get; set; }
    public int Cantidad { get; set; }
    public decimal PrecioCompra { get; set; }
    public decimal PrecioVenta { get; set; }
    public decimal Stock { get; set; }
}

public class CrearCompraHandler : IRequestHandler<CrearCompraRequest, Respuesta<Compra, Mensaje>>
{
    private readonly ICompraServicio _servicio;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CrearCompraHandler(ICompraServicio servicio, IHttpContextAccessor httpContextAccessor)
    {
        _servicio = servicio;
        _httpContextAccessor = httpContextAccessor;
    }

    private string ObtenerClaim(string claimType)
    {
        var claimsPrincipal = _httpContextAccessor.HttpContext.User;
        var claim = claimsPrincipal?.FindFirst(claimType);
        return claim is null ? string.Empty : claim.Value;
    }

    public async Task<Respuesta<Compra, Mensaje>> Handle(CrearCompraRequest request, CancellationToken cancellationToken)
    {
        Compra compra = new()
        {
            IdProveedor = request.IdProveedor,
            TipoComprobante = request.TipoComprobante,
            SerieComprobante = request.SerieComprobante,
            NumComprobante = request.NumComprobante,
            Impuesto = request.Impuesto,
            TotalCompra = request.TotalCompra,
            IdUsuario = Convert.ToInt32(ObtenerClaim(ClaimTypes.Sid)),
            FechaHora = DateTime.Now,
            Detalle = new List<DetalleIngreso>()
        };

        if (request.Detalle is not null && request.Detalle.Count > 0)
        {
            request.Detalle.ForEach(item =>
            {
                compra.Detalle.Add(new DetalleIngreso
                {
                    IdArticulo = item.IdArticulo,
                    Cantidad = item.Cantidad,
                    PrecioCompra = item.PrecioCompra,
                    PrecioVenta = item.PrecioVenta,
                    Stock = item.Stock
                });
            });
        }

        var resultado = await _servicio.CrearCompra(compra);
        return resultado;
    }
}