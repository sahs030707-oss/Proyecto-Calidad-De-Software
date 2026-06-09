using System.ComponentModel.DataAnnotations;

namespace Capa_Entidades
{
    public class Categorias
    {
        public int CategoriaID { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(60, ErrorMessage = "El nombre no puede superar los 60 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [StringLength(255)]
        public string Descripcion { get; set; } = string.Empty;

        public bool Estado { get; set; } = true;
    }
}