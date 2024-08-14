using System;
using api_pos_biblioteca.Modelos;
using api_pos_biblioteca.Modelos.Global;
using api_pos_categoria.Persistencia;

namespace api_pos_categoria.Servicios;

public class CategoriaServicio : ICategoriaServicio
{
    private readonly ICategoriaPersistencia _persistencia;

    public CategoriaServicio(ICategoriaPersistencia persistencia)
    {
        _persistencia = persistencia;
    }

    public async Task<Respuesta<Categoria, Mensaje>> ActualizarCategoria(Categoria request)
    {
        var resultado = await _persistencia.ActualizarCategoria(request);
        return resultado;
    }

    public async Task<Respuesta<Categoria, Mensaje>> CrearCategoria(Categoria request)
    {
        var resultado = await _persistencia.CrearCategoria(request);
        return resultado;
    }

    public async Task<Respuesta<Mensaje, Mensaje>> EliminarCategoria(int id)
    {
        var resultado = await _persistencia.EliminarCategoria(id);
        return resultado;
    }

    public async Task<Respuesta<Categoria, Mensaje>> ObtenerCategoria(int id)
    {
        var resultado = await _persistencia.ObtenerCategoria(id);
        return resultado;
    }

    public async Task<Respuesta<List<Categoria>, Mensaje>> ObtenerCategorias()
    {
        var resultado = await _persistencia.ObtenerCategorias();
        return resultado;
    }
}

public interface ICategoriaServicio
{
    Task<Respuesta<Categoria, Mensaje>> ActualizarCategoria(Categoria request);
    Task<Respuesta<Categoria, Mensaje>> CrearCategoria(Categoria request);
    Task<Respuesta<Mensaje, Mensaje>> EliminarCategoria(int id);
    Task<Respuesta<Categoria, Mensaje>> ObtenerCategoria(int id);
    Task<Respuesta<List<Categoria>, Mensaje>> ObtenerCategorias();
}