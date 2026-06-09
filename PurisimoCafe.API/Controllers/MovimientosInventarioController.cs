using Capa_Entidades;
using Capa_Negocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PurisimoCafe.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MovimientosInventarioController : ControllerBase
    {
        private readonly MovimientosInventarioServicio _movimientosServicio;

        public MovimientosInventarioController(MovimientosInventarioServicio movimientosServicio)
        {
            _movimientosServicio = movimientosServicio;
        }

        [HttpPost]
        public IActionResult InsertarMovimiento([FromBody] MovimientosInventario movimiento)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _movimientosServicio.InsertarMovimiento(movimiento);

                return Ok("Movimiento registrado correctamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al registrar movimiento: {ex.Message}");
            }
        }
    }
}