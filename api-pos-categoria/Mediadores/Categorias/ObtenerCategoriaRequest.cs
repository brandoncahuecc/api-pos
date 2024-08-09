using System;
using api_pos_categoria.Modelos;
using api_pos_categoria.Servicios;
using MediatR;

namespace api_pos_categoria.Mediadores.Categorias;

public class ObtenerCategoriaRequest : IRequest<Categoria>
{
    public int Id { get; set; }
}

public class ObtenerCategoriaHandler : IRequestHandler<ObtenerCategoriaRequest, Categoria>
{
    private readonly ICategoriaServicio _servicio;

    public ObtenerCategoriaHandler(ICategoriaServicio servicio)
    {
        _servicio = servicio;
    }

    public async Task<Categoria> Handle(ObtenerCategoriaRequest request, CancellationToken cancellationToken)
    {
        var resultado = await _servicio.ObtenerCategoria(request.Id);
        return resultado;
    }
}