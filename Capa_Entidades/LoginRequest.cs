using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidades
{
    public class LoginRequest
    {
        public string Correo { get; set; } = string.Empty;
        public string Clave { get; set; } = string.Empty;
    }
}
