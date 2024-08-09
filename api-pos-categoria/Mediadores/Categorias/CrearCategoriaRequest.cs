using api_pos_categoria.Modelos;
using api_pos_categoria.Servicios;
using MediatR;

namespace api_pos_categoria.Mediadores.Categorias;

public class CrearCategoriaRequest : IRequest<Categoria>
{
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
}

public class CrearCategoriaHandler : IRequestHandler<CrearCategoriaRequest, Categoria>
{
    private readonly ICategoriaServicio _servicio;

    public CrearCategoriaHandler(ICategoriaServicio servicio)
    {
        _servicio = servicio;
    }

    public async Task<Categoria> Handle(CrearCategoriaRequest request, CancellationToken cancellationToken)
    {
        Categoria categoria = new()
        {
            Nombre = request.Nombre,
            Descripcion = request.Descripcion
        };

        var resultado = await _servicio.CrearCategoria(categoria);
        return resultado;
    }
}