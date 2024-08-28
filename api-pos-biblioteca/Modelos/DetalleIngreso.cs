using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_pos_biblioteca.Modelos
{
    public class DetalleIngreso
    {
        public int IdDetalleIngreso { get; set; }
        public int IdIngreso { get; set; }
        public int IdArticulo { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal Stock { get; set; }
    }
}
