using StackExchange.Redis;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace api_pos_biblioteca.Modelos
{
    public class Articulo
    {
        public int IdArticulo { get; set; }
        public int IdCategoria { get; set; }
        public int IdMarca { get; set; }
        public int IdLinea { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string AnoDe { get; set; } = string.Empty;
        public string AnoA { get; set; } = string.Empty;
        public decimal Stock { get; set; }
        public decimal StockMinimo { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public string Imagen { get; set; } = string.Empty;
        public int Condicion { get; set; }
        public decimal PrecioVenta { get; set; }
        public string DescuentoPorcentaje { get; set; } = string.Empty;
        public decimal PrecioDescuento { get; set; }
        public string PocerntajePrecioMayorista { get; set; } = string.Empty;
        public string PrecioMayorista { get; set; } = string.Empty;
        public string PocerntajePrecioMinorista { get; set; } = string.Empty;
        public string PrecioMinorista { get; set; } = string.Empty;
        public string PocerntajePrecioMenudeo { get; set; } = string.Empty;
        public string PrecioMenudeo { get; set; } = string.Empty;
        public string PesoProducto { get; set; } = string.Empty;
        public string TipoProducto { get; set; } = string.Empty;
        public int IdUsuario { get; set; }
        public int IdUsuarioUpdate { get; set; }
        public decimal PrecioCompra { get; set; }
        public string EditorialMarca { get; set; } = string.Empty;
        public string Autor { get; set; } = string.Empty;
        public string Yaer { get; set; } = string.Empty;
        public string Paginas { get; set; } = string.Empty;
        public string Edicion { get; set; } = string.Empty;
        public string CodInter { get; set; } = string.Empty;
        public string TipoEstadoLibro { get; set; } = string.Empty;
        public string TipoConsignacion { get; set; } = string.Empty;
        public string Ubicacion { get; set; } = string.Empty;
        public string FechaAdd { get; set; } = string.Empty;
        public string FechaUpdate { get; set; } = string.Empty;
    }
}
