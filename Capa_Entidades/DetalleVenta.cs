using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Capa_Entidades
{
    public class DetalleVenta
    {
        [Key]
        public int DetalleVentaID { get; set; }

        [JsonIgnore]
        public int VentaID { get; set; }

        public int ProductoID { get; set; }

        public string? Nombre { get; set; }

        public decimal PrecioUnitario { get; set; }

        public int Cantidad { get; set; }

        public decimal Subtotal { get; set; }
    }
}