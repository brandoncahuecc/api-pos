using api_pos_biblioteca.Modelos;
using api_pos_biblioteca.Modelos.Global;
using api_pos_categoria.Servicios;
using MediatR;

namespace api_pos_categoria.Mediadores.Categorias;

public class ActualizarCategoriaRequest : IRequest<Respuesta<Categoria, Mensaje>>
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
}

public class ActualizarCategoriaHandler : IRequestHandler<ActualizarCategoriaRequest, Respuesta<Categoria, Mensaje>>
{
    private readonly ICategoriaServicio _servicio;

    public ActualizarCategoriaHandler(ICategoriaServicio servicio)
    {
        _servicio = servicio;
    }

    public async Task<Respuesta<Categoria, Mensaje>> Handle(ActualizarCategoriaRequest request, CancellationToken cancellationToken)
    {
        Categoria categoria = new()
        {
            IdCategoria = request.Id,
            Nombre = request.Nombre,
            Descripcion = request.Descripcion
        };

        var resultado = await _servicio.ActualizarCategoria(categoria);
        return resultado;
    }
}