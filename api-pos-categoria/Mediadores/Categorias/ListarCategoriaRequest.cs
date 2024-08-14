using api_pos_biblioteca.Modelos;
using api_pos_biblioteca.Modelos.Global;
using api_pos_categoria.Servicios;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace api_pos_categoria.Mediadores.Categorias;

public class ListarCategoriaRequest : IRequest<Respuesta<List<Categoria>, Mensaje>>
{

}

public class ListarCategoriaHandler : IRequestHandler<ListarCategoriaRequest, Respuesta<List<Categoria>, Mensaje>>
{
    private readonly ICategoriaServicio _servicio;
    private readonly IDistributedCache _distributed;
    private readonly ILogger<ListarCategoriaHandler> _logger;

    public ListarCategoriaHandler(ICategoriaServicio servicio,
        IDistributedCache distributed,
        ILogger<ListarCategoriaHandler> logger)
    {
        _servicio = servicio;
        _distributed = distributed;
        _logger = logger;
    }

    public async Task<Respuesta<List<Categoria>, Mensaje>> Handle(ListarCategoriaRequest request, CancellationToken cancellationToken)
    {
        int num = 0;
        var result = 8 / num;

        _logger.LogInformation("Inicio la ejecución del listar categoria");
        Respuesta<List<Categoria>, Mensaje> resultado = new();
        try
        {
            _logger.LogInformation("Validando si existen cache de categoria");

            string categoriasCache = await _distributed.GetStringAsync("Categorias");
            if (!string.IsNullOrEmpty(categoriasCache))
            {
                var categorias = JsonConvert.DeserializeObject<List<Categoria>>(categoriasCache);
                return resultado.RespuestaExito(categorias);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fallo al obtener la cache de categoria");
        }

        resultado = await _servicio.ObtenerCategorias();

        try
        {
            DistributedCacheEntryOptions options = new();
            options.SetAbsoluteExpiration(TimeSpan.FromSeconds(30));
            
            _logger.LogInformation("Inicio el seteo de cache de categoria");
            
            if (resultado.Exito && resultado.Objeto.Count > 0)
                _distributed.SetStringAsync("Categorias", JsonConvert.SerializeObject(resultado.Objeto), options);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fallo al setear la cache de categoria");
        }

        return resultado;
    }
}