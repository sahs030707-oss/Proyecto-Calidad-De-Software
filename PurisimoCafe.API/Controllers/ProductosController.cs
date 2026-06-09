using Capa_Entidades;
using Capa_Negocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace PurisimoCafe.API.Controllers
{
   
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductoController : ControllerBase
    {
        private readonly ProductoServicio _productoServicio;
        public ProductoController(ProductoServicio productoServicio)
        {
            _productoServicio = productoServicio;
        }

        /// <summary>
        /// Muestra Todos Los Productos Registrados En El Sistema.
        /// </summary>
        /// returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var lista = _productoServicio.GetProductos();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener productos: {ex.Message}");
            }
        }

        /// <summary>
        /// Muestra Un Producto Registrado Por Su ID.
        /// </summary>
        /// <param name="id">ID del producto</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var producto = _productoServicio.GetProductoById(id);
                if (producto == null) return NotFound($"No se encontró el producto con ID {id}");
                return Ok(producto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener producto: {ex.Message}");
            }
        }

        /// <summary>
        /// Permite Insertar Un Nuevo Producto Al Sistema.
        /// </summary>
        /// returns></returns>

        [HttpPost]
        public IActionResult Post([FromBody] Producto producto)
        {
            try
            {
                if (producto == null) return BadRequest("Datos inválidos");
                if (!ModelState.IsValid) return BadRequest(ModelState);

                _productoServicio.AddProducto(producto);
                return Ok(producto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al insertar producto: {ex.Message}");
            }
        }

        /// <summary>
        /// Permite Actualizar Los Datos De Un Producto.
        /// </summary>
        /// <param name="id">ID del producto</param>
        /// returns></returns>

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Producto producto)
        {
            try
            {
                if (producto == null || producto.ProductoID != id)
                    return BadRequest("Datos incorrectos");
                if (!ModelState.IsValid) return BadRequest(ModelState);

                _productoServicio.UpdateProducto(producto);
                return Ok(producto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar producto: {ex.Message}");
            }
        }

        /// <summary>
        /// Permite Eliminar Un Producto A través De Su ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _productoServicio.DeleteProducto(id);
                return Ok("Producto desactivado correctamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al desactivar producto: {ex.Message}");
            }
        }
    }
}
