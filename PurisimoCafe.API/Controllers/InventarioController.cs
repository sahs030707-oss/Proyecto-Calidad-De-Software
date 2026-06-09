using Capa_Negocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PurisimoCafe.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class InventarioController : ControllerBase
    {
        private readonly InventarioServicio _inventarioServicio;

        public InventarioController(InventarioServicio inventarioServicio)
        {
            _inventarioServicio = inventarioServicio;
        }

        /// <summary>
        /// Muestra El Inventario Completo De Todos Los Productos.
        /// </summary>
        /// <returns></returns>
        // GET: /api/Inventario
        [HttpGet]
        public IActionResult GetInventario()
        {
            var lista = _inventarioServicio.GetInventario();
            return Ok(lista);
        }

        /// <summary>
        /// Permite Aumentar El Stock Si Se Agrega Un Nuevo Producto.
        /// </summary>
        /// <param name="productoId"></param>
        /// <returns></returns>
        // POST: /api/Inventario/AumentarStock
        [HttpPost("AumentarStock")]
        public IActionResult AumentarStock([FromQuery] int productoId, [FromQuery] int cantidad)
        {
            if (productoId <= 0) return BadRequest("Producto inválido");

            if (cantidad <= 0) return BadRequest("La cantidad debe ser mayor a 0");

            _inventarioServicio.AddStock(productoId, cantidad);
            return Ok($"Stock del producto {productoId} aumentado en {cantidad}");
        }
    }
}
