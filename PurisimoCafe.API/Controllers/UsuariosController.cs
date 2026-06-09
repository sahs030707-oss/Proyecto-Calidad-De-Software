using Capa_Entidades;
using Capa_Negocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace PurisimoCafe.API.Controllers
{
    /// <summary>
    /// Modulo De Usuarios.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioServicio _usuarioServicio;

        public UsuariosController(UsuarioServicio usuarioServicio)
        {
            _usuarioServicio = usuarioServicio;
        }

        /// <summary>
        /// Muestra Todos Los Usuarios Que Están Registrados En El Sistema
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var lista = _usuarioServicio.GetUsuarios();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener usuarios: {ex.Message}");
            }
        }
        /// <summary>
        /// Muestra Un Usuario Registrado Por Su ID
        /// </summary>

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var usuario = _usuarioServicio.GetUsuarioById(id);
                if (usuario == null) return NotFound($"No se encontró el usuario con ID {id}");
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener usuario: {ex.Message}");
            }
        }
        /// <summary>
        /// Permite Insertar Un Usuario Nuevo Al Sistema
        /// </summary>

        [HttpPost]
        public IActionResult Post([FromBody] Usuario user, [FromQuery] string Contraseña)
        {
            try
            {
                if (user == null || string.IsNullOrEmpty(Contraseña))
                    return BadRequest("Datos inválidos");

                _usuarioServicio.AddUsuario(user, Contraseña);
                return Ok("Usuario insertado correctamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al insertar usuario: {ex.Message}");
            }
        }

        /// <summary>
        /// Permite Actualizar A Un Usuario Existente
        /// </summary>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Usuario usuario, [FromQuery] string? Contraseña = null)
        {
            try
            {
                if (usuario == null || usuario.UsuarioID != id)
                    return BadRequest("Datos incorrectos");

                _usuarioServicio.UpdateUsuario(usuario, Contraseña);
                return Ok("Usuario actualizado correctamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar usuario: {ex.Message}");
            }
        }
        /// <summary>
        /// Permite Eliminar Un Usuario A través De Su ID
        /// </summary>

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _usuarioServicio.DeleteUsuario(id);
                return Ok("Usuario eliminado correctamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar usuario: {ex.Message}");
            }
        }
    }
}
