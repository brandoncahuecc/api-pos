using api_pos_categoria.Modelos.Global;
using api_pos_categoria.Modelos;
using api_pos_categoria.Servicios;
using MediatR;

namespace api_pos_categoria.Mediadores.Categorias;

public class EliminarCategoriaRequest : IRequest<Respuesta<Mensaje, Mensaje>>
{
    public int Id { get; set; }
}

public class EliminarCategoriaHandler : IRequestHandler<EliminarCategoriaRequest, Respuesta<Mensaje, Mensaje>>
{
    private readonly ICategoriaServicio _servicio;

    public EliminarCategoriaHandler(ICategoriaServicio servicio)
    {
        _servicio = servicio;
    }

    public async Task<Respuesta<Mensaje, Mensaje>> Handle(EliminarCategoriaRequest request, CancellationToken cancellationToken)
    {
        var resultado = await _servicio.EliminarCategoria(request.Id);
        return resultado;
    }
}