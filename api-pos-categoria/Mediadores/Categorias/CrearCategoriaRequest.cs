using api_pos_biblioteca.Modelos;
using api_pos_biblioteca.Modelos.Global;
using api_pos_categoria.Servicios;
using MediatR;

namespace api_pos_categoria.Mediadores.Categorias;

public class CrearCategoriaRequest : IRequest<Respuesta<Categoria, Mensaje>>
{
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
}

public class CrearCategoriaHandler : IRequestHandler<CrearCategoriaRequest, Respuesta<Categoria, Mensaje>>
{
    private readonly ICategoriaServicio _servicio;

    public CrearCategoriaHandler(ICategoriaServicio servicio)
    {
        _servicio = servicio;
    }

    public async Task<Respuesta<Categoria, Mensaje>> Handle(CrearCategoriaRequest request, CancellationToken cancellationToken)
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