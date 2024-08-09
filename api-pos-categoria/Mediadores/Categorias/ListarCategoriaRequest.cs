using api_pos_categoria.Modelos;
using api_pos_categoria.Servicios;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace api_pos_categoria.Mediadores.Categorias;

public class ListarCategoriaRequest : IRequest<List<Categoria>>
{

}

public class ListarCategoriaHandler : IRequestHandler<ListarCategoriaRequest, List<Categoria>>
{
    private readonly ICategoriaServicio _servicio;
    private readonly IDistributedCache _distributed;

    public ListarCategoriaHandler(ICategoriaServicio servicio, IDistributedCache distributed)
    {
        _servicio = servicio;
        _distributed = distributed;
    }

    public async Task<List<Categoria>> Handle(ListarCategoriaRequest request, CancellationToken cancellationToken)
    {
        try
        {
            string categoriasCache = await _distributed.GetStringAsync("Categorias");
            if (!string.IsNullOrEmpty(categoriasCache))
            {
                var categorias = JsonConvert.DeserializeObject<List<Categoria>>(categoriasCache);
                return categorias;
            }
        }
        catch (Exception ex)
        {

        }

        var resultado = await _servicio.ObtenerCategorias();

        try
        {
            DistributedCacheEntryOptions options = new();
            options.SetAbsoluteExpiration(TimeSpan.FromSeconds(30));

            if (resultado.Count > 0)
                _distributed.SetStringAsync("Categorias", JsonConvert.SerializeObject(resultado), options);
        }
        catch (Exception ex)
        {

        }

        await Task.Delay(5000);

        return resultado;
    }
}