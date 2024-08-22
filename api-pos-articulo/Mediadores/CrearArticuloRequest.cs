using api_pos_articulo.Servicios;
using api_pos_biblioteca.Modelos;
using api_pos_biblioteca.Modelos.Global;
using MediatR;
using System.Security.Claims;

namespace api_pos_articulo.Mediadores
{
    public class CrearArticuloRequest : IRequest<Respuesta<Articulo, Mensaje>>
    {
        public int IdCategoria { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public decimal Stock { get; set; }
        public decimal StockMinimo { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public string Imagen { get; set; } = string.Empty;
        public decimal PrecioVenta { get; set; }
        public decimal PrecioCompra { get; set; }
    }

    public class CrearArticuloHandler : IRequestHandler<CrearArticuloRequest, Respuesta<Articulo, Mensaje>>
    {
        private readonly IArticuloServicio _servicio;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CrearArticuloHandler(IArticuloServicio servicio, IHttpContextAccessor httpContextAccessor)
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

        public async Task<Respuesta<Articulo, Mensaje>> Handle(CrearArticuloRequest request, CancellationToken cancellationToken)
        {
            Articulo articulo = new()
            {
                IdCategoria = request.IdCategoria,
                Codigo = request.Codigo,
                Nombre = request.Nombre,
                Stock = request.Stock,
                StockMinimo = request.StockMinimo,
                Descripcion = request.Descripcion,
                Imagen = request.Imagen,
                PrecioVenta = request.PrecioVenta,
                IdUsuario = Convert.ToInt32(ObtenerClaim(ClaimTypes.Sid)),
                PrecioCompra = request.PrecioCompra,
                FechaAdd = DateTime.Now.ToString("yyyy-MM-dd")
            };

            var resultado = await _servicio.CrearArticulo(articulo);
            return resultado;
        }
    }
}
