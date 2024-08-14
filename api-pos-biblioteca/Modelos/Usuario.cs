using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace api_pos_biblioteca.Modelos
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string TipoDocumento { get; set; } = string.Empty;
        public string NumDocumento { get; set; } = string.Empty;
        public string direccion { get; set; } = string.Empty;
        public string telefono { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string cargo { get; set; } = string.Empty;
        public string login { get; set; } = string.Empty;
        public string clave { get; set; } = string.Empty;
        public string imagen { get; set; } = string.Empty;
        public int idsucursal { get; set; }
        public int condicion { get; set; }
        public Tokens Tokens { get; set; }
    }
}
