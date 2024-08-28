using api_pos_biblioteca.Modelos;
using api_pos_biblioteca.Modelos.Global;
using api_pos_compra.Servicios;
using MediatR;

namespace api_pos_compra.Mediadores;

public class CrearCompraRequest : IRequest<Respuesta<Compra, Mensaje>>
{
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
}

public class CrearCompraHandler : IRequestHandler<CrearCompraRequest, Respuesta<Compra, Mensaje>>
{
    private readonly ICompraServicio _servicio;

    public CrearCompraHandler(ICompraServicio servicio)
    {
        _servicio = servicio;
    }

    public async Task<Respuesta<Compra, Mensaje>> Handle(CrearCompraRequest request, CancellationToken cancellationToken)
    {
        Compra compra = new();

        var resultado = await _servicio.CrearCompra(compra);
        return resultado;
    }
}