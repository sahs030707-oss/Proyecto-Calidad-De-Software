using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        public int UsuarioID { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Correo { get; set; } = string.Empty;

        public string Contraseña { get; set; } = string.Empty;

        public int RolID { get; set; }

        // Relación con Roles
        public Roles Rol { get; set; }

    }
}
