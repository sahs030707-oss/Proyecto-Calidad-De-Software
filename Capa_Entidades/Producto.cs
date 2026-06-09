using System.ComponentModel.DataAnnotations;

namespace Capa_Entidades
{
    public class Producto
    {
        [Key]
        public int ProductoID { get; set; }

        public int CategoriaID { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(60, ErrorMessage = "El nombre no puede contener más de 60 caracteres.")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [StringLength(255)]
        public string? Descripcion { get; set; }

        public bool Estado { get; set; } = true;

        [Required]
        public decimal CostoProduccion { get; set; }

        [Required]
        public decimal PrecioUnitario { get; set; }
    }
}