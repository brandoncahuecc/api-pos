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
        public int? IdUsuario { get; set; }
        public int? IdusuarioUpdate { get; set; }
        public string TipoComprobante { get; set; }
        public string SerieComprobante { get; set; }
        public string NumComprobante { get; set; }
        public DateTime? FechaHora { get; set; }
        public Decimal? Impuesto { get; set; }
        public Decimal? TotalCompra { get; set; }
        public string Estado { get; set; }
        public string FormaPago { get; set; }
        public string DiasCredito { get; set; }
        public DateTime? FechaHoraPagoCredito { get; set; }
        public Decimal ValorPagar { get; set; }
        public Decimal SaldoIngreso { get; set; }
        public string TipoPago { get; set; }
        public string NoCheque { get; set; }
        public DateTime? FechaHoraGeneracionPago { get; set; }
        public string TipoBanco { get; set; }
        public string NumeroBoleta { get; set; }
        public string ReciboCajaNumero { get; set; }
        public string DireccionEntregaOrdenCompra { get; set; }
        public DateTime? FechaEntregaOrdenCompra { get; set; }
        public string ObservacionOrdenCompra { get; set; }
        public string UsuaruiModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string MotivoModificacion { get; set; }
        public string NumCorrelativo { get; set; }
        public List<DetalleIngreso> Detalle { get; set; }
    }
}
