using System;
using api_pos_categoria.Modelos;
using api_pos_categoria.Modelos.Global;
using api_pos_categoria.Servicios;
using MediatR;

namespace api_pos_categoria.Mediadores.Categorias;

public class ObtenerCategoriaRequest : IRequest<Respuesta<Categoria, Mensaje>>
{
    public int Id { get; set; }
}

public class ObtenerCategoriaHandler : IRequestHandler<ObtenerCategoriaRequest, Respuesta<Categoria, Mensaje>>
{
    private readonly ICategoriaServicio _servicio;

    public ObtenerCategoriaHandler(ICategoriaServicio servicio)
    {
        _servicio = servicio;
    }

    public async Task<Respuesta<Categoria, Mensaje>> Handle(ObtenerCategoriaRequest request, CancellationToken cancellationToken)
    {
        var resultado = await _servicio.ObtenerCategoria(request.Id);
        return resultado;
    }
}