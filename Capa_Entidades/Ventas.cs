using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace Capa_Entidades
{
    [Table("Ventas")]
    public class Ventas
    {
        [Key]
        public int VentaID { get; set; }

        [Required]
        public DateTime FechaVenta { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Total { get; set; }

        
        public List<DetalleVenta> Detalles { get; set; } = new();
    }
}