using System;

namespace api_pos_biblioteca.Modelos;

public class Categoria
{
    public int IdCategoria { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public int Condicion { get; set; }
}
