// PU-008 — InsertarCategoria en CategoriaServicio
using Capa_Datos;
using Capa_Entidades;
using Capa_Negocio;
using Moq;

namespace PurisimoCafe.Tests
{
    public class CategoriaServicioTests
    {
        [Fact]
        public void InsertarCategoria_ConCategoriaValida_LlamaRepositorio()
        {
            var mockDatos = new Mock<ICategoriaDatos>();
            mockDatos.Setup(d => d.InsertarCategoria(It.IsAny<Categorias>()));

            var servicio = new CategoriaServicio(mockDatos.Object);
            var categoria = new Categorias
            {
                Nombre = "Bebidas Calientes",
                Descripcion = "Café, té y chocolate caliente",
                Estado = true
            };

            servicio.InsertarCategoria(categoria);

            mockDatos.Verify(d => d.InsertarCategoria(It.Is<Categorias>(c =>
                c.Nombre == "Bebidas Calientes" &&
                c.Descripcion == "Café, té y chocolate caliente" &&
                c.Estado == true)), Times.Once);
        }
    }
}
