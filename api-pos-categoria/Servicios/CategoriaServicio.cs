using System;
using api_pos_categoria.Modelos;
using api_pos_categoria.Persistencia;

namespace api_pos_categoria.Servicios;

public class CategoriaServicio : ICategoriaServicio
{
    private readonly ICategoriaPersistencia _persistencia;

    public CategoriaServicio(ICategoriaPersistencia persistencia)
    {
        _persistencia = persistencia;
    }

    public async Task<Categoria> ActualizarCategoria(Categoria request)
    {
        var resultado = await _persistencia.ActualizarCategoria(request);
        return resultado;
    }

    public async Task<Categoria> CrearCategoria(Categoria request)
    {
        var resultado = await _persistencia.CrearCategoria(request);
        return resultado;
    }

    public async Task<bool> EliminarCategoria(int id)
    {
        var resultado = await _persistencia.EliminarCategoria(id);
        return resultado;
    }

    public async Task<Categoria> ObtenerCategoria(int id)
    {
        var resultado = await _persistencia.ObtenerCategoria(id);
        return resultado;
    }

    public async Task<List<Categoria>> ObtenerCategorias()
    {
        var resultado = await _persistencia.ObtenerCategorias();
        return resultado;
    }
}

public interface ICategoriaServicio
{
    Task<Categoria> ActualizarCategoria(Categoria request);
    Task<Categoria> CrearCategoria(Categoria request);
    Task<bool> EliminarCategoria(int id);
    Task<Categoria> ObtenerCategoria(int id);
    Task<List<Categoria>> ObtenerCategorias();
}