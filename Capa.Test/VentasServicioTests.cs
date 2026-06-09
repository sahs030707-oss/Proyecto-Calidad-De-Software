// PU-003 & PU-004 — Pruebas de VentaServicio
using Capa_Datos;
using Capa_Entidades;
using Capa_Negocio;
using Moq;

namespace PurisimoCafe.Tests
{
    public class VentaServicioTests
    {
        private Mock<IVentaDatos> CreateMockDatos()
        {
            return new Mock<IVentaDatos>();
        }

        private VentaServicio CreateServicio(Mock<IVentaDatos> mockDatos)
        {
            return new VentaServicio(mockDatos.Object);
        }

        [Fact]
        public void RegistrarVenta_ConDetallesValidos_CalculaTotalCorrectamente()
        {
            var mockDatos = CreateMockDatos();
            mockDatos.Setup(d => d.RegistrarVenta(It.IsAny<Ventas>(), It.IsAny<List<DetalleVenta>>()));

            var servicio = CreateServicio(mockDatos);
            var venta = new Ventas();
            var detalles = new List<DetalleVenta>
            {
                new DetalleVenta { ProductoID = 1, PrecioUnitario = 45.00m, Cantidad = 2 },
                new DetalleVenta { ProductoID = 2, PrecioUnitario = 30.00m, Cantidad = 1 }
            };

            servicio.RegistrarVenta(venta, detalles);

            Assert.Equal(120.00m, venta.Total);
            Assert.Equal(90.00m, detalles[0].Subtotal);
            Assert.Equal(30.00m, detalles[1].Subtotal);
            Assert.True(venta.FechaVenta > DateTime.MinValue);
            mockDatos.Verify(d => d.RegistrarVenta(It.IsAny<Ventas>(), It.IsAny<List<DetalleVenta>>()), Times.Once);
        }

        [Fact]
        public void RegistrarVenta_SinDetalles_LanzaExcepcion()
        {
            var mockDatos = CreateMockDatos();
            var servicio = CreateServicio(mockDatos);
            var venta = new Ventas();
            var detalles = new List<DetalleVenta>();

            var ex = Assert.Throws<Exception>(() => servicio.RegistrarVenta(venta, detalles));
            Assert.Equal("La venta debe contener al menos un producto.", ex.Message);
        }
    }
}
