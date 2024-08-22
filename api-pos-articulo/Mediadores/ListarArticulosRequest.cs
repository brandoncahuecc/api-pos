using api_pos_biblioteca.Modelos.Global;
using api_pos_biblioteca.Modelos;
using MediatR;
using api_pos_articulo.Servicios;

namespace api_pos_articulo.Mediadores
{
    public class ListarArticulosRequest : IRequest<Respuesta<List<Articulo>, Mensaje>>
    {

    }

    public class ListarArticulosHandler : IRequestHandler<ListarArticulosRequest, Respuesta<List<Articulo>, Mensaje>>
    {
        private readonly IArticuloServicio _servicio;

        public ListarArticulosHandler(IArticuloServicio servicio)
        {
            _servicio = servicio;
        }

        public async Task<Respuesta<List<Articulo>, Mensaje>> Handle(ListarArticulosRequest request, CancellationToken cancellationToken)
        {
            var resultado = await _servicio.ObtenerArticulos();
            return resultado;
        }
    }
}
