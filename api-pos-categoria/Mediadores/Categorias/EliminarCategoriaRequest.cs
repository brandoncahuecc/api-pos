using api_pos_categoria.Servicios;
using MediatR;

namespace api_pos_categoria.Mediadores.Categorias;

public class EliminarCategoriaRequest : IRequest<bool>
{
    public int Id { get; set; }
}

public class EliminarCategoriaHandler : IRequestHandler<EliminarCategoriaRequest, bool>
{
    private readonly ICategoriaServicio _servicio;

    public EliminarCategoriaHandler(ICategoriaServicio servicio)
    {
        _servicio = servicio;
    }

    public async Task<bool> Handle(EliminarCategoriaRequest request, CancellationToken cancellationToken)
    {
        var resultado = await _servicio.EliminarCategoria(request.Id);
        return resultado;
    }
}