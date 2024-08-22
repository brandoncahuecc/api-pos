using api_pos_biblioteca.Modelos.Global;
using api_pos_biblioteca.Modelos;
using api_pos_articulo.Persistencia;

namespace api_pos_articulo.Servicios
{
    public class ArticuloServicio : IArticuloServicio
    {
        private readonly IArticuloPersistencia _persistencia;

        public ArticuloServicio(IArticuloPersistencia persistencia)
        {
            _persistencia = persistencia;
        }

        public async Task<Respuesta<Articulo, Mensaje>> ActualizarArticulo(Articulo request)
        {
            throw new NotImplementedException();
        }

        public async Task<Respuesta<Articulo, Mensaje>> CrearArticulo(Articulo request)
        {
            var resultado = await _persistencia.CrearArticulo(request);
            return resultado;
        }

        public async Task<Respuesta<Mensaje, Mensaje>> EliminarArticulo(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Respuesta<Articulo, Mensaje>> ObtenerArticulo(int id)
        {
            var respuesta = await _persistencia.ObtenerArticulo(id);
            return respuesta;
        }

        public async Task<Respuesta<List<Articulo>, Mensaje>> ObtenerArticulos()
        {
            var respuesta = await _persistencia.ObtenerArticulos();
            return respuesta;
        }
    }

    public interface IArticuloServicio
    {
        Task<Respuesta<Articulo, Mensaje>> ActualizarArticulo(Articulo request);
        Task<Respuesta<Articulo, Mensaje>> CrearArticulo(Articulo request);
        Task<Respuesta<Mensaje, Mensaje>> EliminarArticulo(int id);
        Task<Respuesta<Articulo, Mensaje>> ObtenerArticulo(int id);
        Task<Respuesta<List<Articulo>, Mensaje>> ObtenerArticulos();
    }
}
