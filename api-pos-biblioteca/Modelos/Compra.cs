using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_pos_biblioteca.Modelos
{
    public class Compra
    {
        public int IdIngreso { get; set; }
        public int IdProveedor { get; set; }
        public int IdUsuario { get; set; }
        public int IdusuarioUpdate { get; set; }
        public string TipoComprobante { get; set; } = string.Empty;
        public string SerieComprobante { get; set; } = string.Empty;
        public string NumComprobante { get; set; } = string.Empty;
        public DateTime FechaHora { get; set; }
        public Decimal Impuesto { get; set; }
        public Decimal TotalCompra { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string FormaPago { get; set; } = string.Empty;
        public string DiasCredito { get; set; } = string.Empty;
        public DateTime FechaHoraPagoCredito { get; set; }
        public Decimal ValorPagar { get; set; }
        public Decimal SaldoIngreso { get; set; }
        public string TipoPago { get; set; } = string.Empty;
        public string NoCheque { get; set; } = string.Empty;
        public DateTime? FechaHoraGeneracionPago { get; set; }
        public string TipoBanco { get; set; } = string.Empty;
        public string NumeroBoleta { get; set; } = string.Empty;
        public string ReciboCajaNumero { get; set; } = string.Empty;
        public string DireccionEntregaOrdenCompra { get; set; } = string.Empty;
        public DateTime FechaEntregaOrdenCompra { get; set; }
        public string ObservacionOrdenCompra { get; set; } = string.Empty;
        public string UsuaruiModificacion { get; set; } = string.Empty;
        public DateTime FechaModificacion { get; set; }
        public string MotivoModificacion { get; set; } = string.Empty;
        public string NumCorrelativo { get; set; } = string.Empty;
        public List<DetalleIngreso> Detalle { get; set; } = new();
    }
}
