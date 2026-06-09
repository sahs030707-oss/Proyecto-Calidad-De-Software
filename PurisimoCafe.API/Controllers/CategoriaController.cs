using Microsoft.AspNetCore.Mvc;
using Capa_Entidades;
using Capa_Negocio;
using Microsoft.AspNetCore.Authorization;

namespace PurisimoCafe.API.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CategoriaController : ControllerBase
    {
        private readonly CategoriaServicio _categoriaServicio;

        public CategoriaController(CategoriaServicio categoriaServicio)
        {
            _categoriaServicio = categoriaServicio;
        }

        /// <summary>
        /// Muestra Todas Las Categorias Existentes En El Sistema.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ObtenerCategorias()
        {
            var categorias = _categoriaServicio.ObtenerCategorias();
            return Ok(categorias);
        }

        /// <summary>
        /// Muestra Una Categoria Especifica Del Sistema Por Su ID.
        /// </summary>
        /// <param name="id">ID de la categoría</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult ObtenerCategoriaPorId(int id)
        {
            var categoria = _categoriaServicio.ObtenerCategoriaPorId(id);

            if (categoria == null)
                return NotFound($"No se encontró la categoría con ID {id}");

            return Ok(categoria);
        }

        /// <summary>
        /// Permite Insertar Una Nueva Categoria Al Sistema.
        /// </summary>
        /// <param name="categoria"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult InsertarCategoria([FromBody] Categorias categoria)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _categoriaServicio.InsertarCategoria(categoria);
            return Ok(categoria);
        }

        /// <summary>
        /// Permite Borrar Una Categoria Del Sistema Por Su ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult EliminarCategoria(int id)
        {
            _categoriaServicio.EliminarCategoria(id);
            return Ok("Categoría Desactivada correctamente");
        }
    }
}
