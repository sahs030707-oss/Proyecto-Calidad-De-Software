using Capa_Entidades;
using Capa_Negocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace PurisimoCafe.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class VentaController : ControllerBase
    {
        private readonly VentaServicio _ventaServicio;

        public VentaController(VentaServicio ventaServicio)
        {
            _ventaServicio = ventaServicio;
        }

        /// <summary>
        /// Registra Una Nueva Venta Con Sus Detalles.
        /// </summary>
        [HttpPost("Registrar")]
        public IActionResult RegistrarVenta([FromBody] VentaRequest request)
        {
            try
            {
                if (request == null || request.Detalles == null || request.Detalles.Count == 0)
                    return BadRequest("Debe enviar los detalles de la venta.");

                var venta = new Ventas();
                _ventaServicio.RegistrarVenta(venta, request.Detalles);

                return Ok("Venta registrada correctamente");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Stock insuficiente") ||(ex.InnerException != null && ex.InnerException.Message.Contains("Stock insuficiente")))
                {
                    return BadRequest("No hay stock suficiente para uno o más productos.");
                }
                return StatusCode(500, $"Error al registrar la venta: {ex.Message}");
            }
        }
        /// <summary>
        /// Muestra Todas Las Ventas Registradas En El Sistema.
        /// </summary>

        [HttpGet]
        public IActionResult ObtenerVentas()
        {
            var ventas = _ventaServicio.ObtenerVentas();
            return Ok(ventas);
        }


        /// <summary>
        /// Muestra Una Venta Registrada Por Su ID.
        /// </summary>  
        [HttpGet("{id}")]
        public IActionResult ObtenerVentaPorId(int id)
        {
            if (id <= 0)
                return BadRequest("ID inválido");

            var venta = _ventaServicio.ObtenerVentaPorId(id);
            if (venta == null)
                return NotFound($"No se encontró la venta con ID {id}");

            return Ok(venta);
        }
    }

    public class VentaRequest
    {
        public List<DetalleVenta> Detalles { get; set; } = new();
    }
}