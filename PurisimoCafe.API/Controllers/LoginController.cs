using Capa_Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace PurisimoCafe.API.Controllers
{
    /// <summary>
    /// Inicio De Sesión Y Generación De Tokens JWT
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly PC_DBContext _context;
        private readonly IConfiguration _config;

        public LoginController(PC_DBContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        /// <summary>
        ///  Permite A Un Usuario Iniciar Sesión Y Obtener Un Token JWT De Autenticación.
        /// </summary>
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // 🔹 1. Buscar usuario en la BD
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Correo == request.Correo);

            if (usuario == null)
                return Unauthorized("Usuario no encontrado");

            // 🔹 2. Validar clave
            if (usuario.Contraseña != request.Clave)
                return Unauthorized("Credenciales inválidas");

            // 🔹 3. Generar Token
            var jwtSettings = _config.GetSection("Jwt");
            var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);


            var claims = new[]
             {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Correo),
                new Claim("id", usuario.RolID.ToString()),
                new Claim("rol", usuario.RolID.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1), // Token válido por 1 hora
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new
            {
                Token = tokenString,
                Usuario = usuario.Nombre,
                Correo = usuario.Correo,
                RolId = usuario.RolID,
            });
        }
    }
}