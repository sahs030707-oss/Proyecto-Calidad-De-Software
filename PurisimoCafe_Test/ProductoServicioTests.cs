// PU-005 & PU-006 — Pruebas de ProductoServicio
using Capa_Datos;
using Capa_Entidades;
using Capa_Negocio;
using Microsoft.Extensions.Configuration;
using Moq;

namespace PurisimoCafe.Tests
{
    public class ProductoServicioTests
    {
        private Mock<ProductoDatos> CreateMockDatos()
        {
            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "ConnectionStrings:DefaultConnection", "Server=.;" }
                })
                .Build();

            return new Mock<ProductoDatos>(config);
        }

        private ProductoServicio CreateServicio(Mock<ProductoDatos> mockDatos)
        {
            return new ProductoServicio(mockDatos.Object);
        }

        [Fact]
        public void AddProducto_ConDatosValidos_LlamaInsertar()
        {
            var mockDatos = CreateMockDatos();
            mockDatos.Setup(d => d.Insertar(It.IsAny<Producto>()));

            var servicio = CreateServicio(mockDatos);
            var producto = new Producto
            {
                Nombre = "Espresso",
                CategoriaID = 1,
                PrecioUnitario = 45.00m,
                CostoProduccion = 15.00m,
                Estado = true
            };

            servicio.AddProducto(producto);

            mockDatos.Verify(d => d.Insertar(It.Is<Producto>(p =>
                p.Nombre == "Espresso" &&
                p.CategoriaID == 1 &&
                p.PrecioUnitario == 45.00m &&
                p.CostoProduccion == 15.00m &&
                p.Estado == true)), Times.Once);
        }

        [Fact]
        public void AddProducto_ConProductoNulo_LanzaArgumentNullException()
        {
            var mockDatos = CreateMockDatos();
            var servicio = CreateServicio(mockDatos);

            Assert.Throws<ArgumentNullException>(() => servicio.AddProducto(null!));
        }
    }
}
