using System.ComponentModel.DataAnnotations;

namespace Capa_Entidades
{
    public class MovimientosInventario
    {
        public int MovimientoID { get; set; }

        [Required]
        public int ProductoID { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0.")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "El motivo es obligatorio.")]
        [StringLength(255)]
        public string? Motivo { get; set; }

        public DateTime Fecha { get; set; }
    }
}