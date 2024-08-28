using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace api_pos_biblioteca.Modelos
{
    public class Persona
    {
        public int IdPersona { get; set; }
        public string TipoPersona { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string TipoDocumento { get; set; } = string.Empty;
        public string NumDocumento { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string TipoCliente { get; set; } = string.Empty;
    }
}
