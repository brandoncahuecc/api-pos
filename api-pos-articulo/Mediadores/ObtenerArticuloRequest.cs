using api_pos_biblioteca.Modelos.Global;
using api_pos_biblioteca.Modelos;
using MediatR;
using api_pos_articulo.Servicios;

namespace api_pos_articulo.Mediadores
{
    public class ObtenerArticuloRequest : IRequest<Respuesta<Articulo, Mensaje>>
    {
        public int Id { get; set; }
    }

    public class ObtenerArticuloHandler : IRequestHandler<ObtenerArticuloRequest, Respuesta<Articulo, Mensaje>>
    {
        private readonly IArticuloServicio _servicio;

        public ObtenerArticuloHandler(IArticuloServicio servicio)
        {
            _servicio = servicio;
        }

        public async Task<Respuesta<Articulo, Mensaje>> Handle(ObtenerArticuloRequest request, CancellationToken cancellationToken)
        {
            var resultado = await _servicio.ObtenerArticulo(request.Id);
            return resultado;
        }
    }
}
