using System;
using api_pos_biblioteca.Modelos;
using api_pos_biblioteca.Modelos.Global;
using api_pos_compra.Persistencia;

namespace api_pos_compra.Servicios;

public class CompraServicio : ICompraServicio
{
    private readonly ICompraPersistencia _persistencia;

    public CompraServicio(ICompraPersistencia persistencia)
    {
        _persistencia = persistencia;
    }

    public async Task<Respuesta<Compra, Mensaje>> CrearCompra(Compra request)
    {
        var resultado = await _persistencia.CrearCompra(request);
        return resultado;
    }

    public async Task<Respuesta<Mensaje, Mensaje>> EliminarCompra(int id)
    {
        var resultado = await _persistencia.EliminarCompra(id);
        return resultado;
    }

    public async Task<Respuesta<List<Compra>, Mensaje>> ObtenerCompras()
    {
        var resultado = await _persistencia.ObtenerCompras();
        return resultado;
    }
}

public interface ICompraServicio
{
    Task<Respuesta<Compra, Mensaje>> CrearCompra(Compra request);
    Task<Respuesta<Mensaje, Mensaje>> EliminarCompra(int id);
    Task<Respuesta<List<Compra>, Mensaje>> ObtenerCompras();
}