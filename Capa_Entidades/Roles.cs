using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Capa_Entidades
{
    [Table("Roles")]
    public class Roles
    {
        [Key]
        [JsonIgnore]
        public int RolID { get; set; }
        public string NombreRol { get; set; } = string.Empty;

        [JsonIgnore]
        public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
    }
}
