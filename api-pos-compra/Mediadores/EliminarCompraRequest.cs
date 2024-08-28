using api_pos_biblioteca.Modelos.Global;
using api_pos_biblioteca.Modelos;
using api_pos_compra.Servicios;
using MediatR;

namespace api_pos_compra.Mediadores;

public class EliminarCompraRequest : IRequest<Respuesta<Mensaje, Mensaje>>
{
    public int Id { get; set; }
}

public class EliminarCompraHandler : IRequestHandler<EliminarCompraRequest, Respuesta<Mensaje, Mensaje>>
{
    private readonly ICompraServicio _servicio;

    public EliminarCompraHandler(ICompraServicio servicio)
    {
        _servicio = servicio;
    }

    public async Task<Respuesta<Mensaje, Mensaje>> Handle(EliminarCompraRequest request, CancellationToken cancellationToken)
    {
        var resultado = await _servicio.EliminarCompra(request.Id);
        return resultado;
    }
}