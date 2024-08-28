using api_pos_biblioteca.Modelos;
using api_pos_biblioteca.Modelos.Global;
using api_pos_compra.Servicios;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace api_pos_compra.Mediadores;

public class ListarCompraRequest : IRequest<Respuesta<List<Compra>, Mensaje>>
{

}

public class ListarCompraHandler : IRequestHandler<ListarCompraRequest, Respuesta<List<Compra>, Mensaje>>
{
    private readonly ICompraServicio _servicio;
    private readonly IDistributedCache _distributed;
    private readonly ILogger<ListarCompraHandler> _logger;

    public ListarCompraHandler(ICompraServicio servicio,
        IDistributedCache distributed,
        ILogger<ListarCompraHandler> logger)
    {
        _servicio = servicio;
        _distributed = distributed;
        _logger = logger;
    }

    public async Task<Respuesta<List<Compra>, Mensaje>> Handle(ListarCompraRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Inicio la ejecuci�n del listar categoria");
        Respuesta<List<Compra>, Mensaje> resultado = new();
        try
        {
            _logger.LogInformation("Validando si existen cache de categoria");

            string categoriasCache = await _distributed.GetStringAsync("Compras");
            if (!string.IsNullOrEmpty(categoriasCache))
            {
                var categorias = JsonConvert.DeserializeObject<List<Compra>>(categoriasCache);
                return resultado.RespuestaExito(categorias);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fallo al obtener la cache de categoria");
        }

        resultado = await _servicio.ObtenerCompras();

        try
        {
            DistributedCacheEntryOptions options = new();
            options.SetAbsoluteExpiration(TimeSpan.FromSeconds(30));

            _logger.LogInformation("Inicio el seteo de cache de categoria");

            if (resultado.Exito && resultado.Objeto.Count > 0)
                _distributed.SetStringAsync("Compras", JsonConvert.SerializeObject(resultado.Objeto), options);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fallo al setear la cache de categoria");
        }

        return resultado;
    }
}