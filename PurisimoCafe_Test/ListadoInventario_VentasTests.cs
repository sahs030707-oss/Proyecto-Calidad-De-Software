// PU-011 & PU-012 — Inventario y Ventas (Listados)
using Capa_Datos;
using Capa_Entidades;
using Capa_Negocio;
using Microsoft.Extensions.Configuration;
using Moq;

namespace PurisimoCafe.Tests
{
    public class ListadoInventario_VentasTests
    {
        private IConfiguration CreateConfig() =>
            new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "ConnectionStrings:DefaultConnection", "Server=.;" }
                })
                .Build();

        [Fact]
        public void GetInventario_RetornaInventarioConStockActual()
        {
            var mockDatos = new Mock<InventarioDatos>(CreateConfig());
            mockDatos.Setup(d => d.ObtenerInventario())
                     .Returns(new List<Inventario>
                     {
                         new Inventario
                         {
                             InventarioID = 1,
                             ProductoID = 1,
                             NombreProducto = "Espresso",
                             StockActual = 50,
                             PrecioUnitario = 45.00m,
                             Estado = true,
                             Fecha = DateTime.Now
                         },
                         new Inventario
                         {
                             InventarioID = 2,
                             ProductoID = 2,
                             NombreProducto = "Americano",
                             StockActual = 30,
                             PrecioUnitario = 35.00m,
                             Estado = true,
                             Fecha = DateTime.Now
                         }
                     });

            var servicio = new InventarioServicio(mockDatos.Object);
            var resultado = new List<Inventario>(servicio.GetInventario());

            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count);
            Assert.All(resultado, i => Assert.True(i.StockActual > 0));
            Assert.All(resultado, i => Assert.True(i.PrecioUnitario > 0));
        }

        [Fact]
        public void ObtenerVentas_RetornaHistorialCompleto()
        {
            var mockDatos = new Mock<VentaDatos>(CreateConfig());
            mockDatos.Setup(d => d.ObtenerVentas())
                     .Returns(new List<Ventas>
                     {
                         new Ventas { VentaID = 1, Total = 120.00m, FechaVenta = DateTime.Now.AddDays(-2) },
                         new Ventas { VentaID = 2, Total = 75.50m, FechaVenta = DateTime.Now.AddDays(-1) },
                         new Ventas { VentaID = 3, Total = 200.00m, FechaVenta = DateTime.Now }
                     });

            var servicio = new VentaServicio(mockDatos.Object);
            var resultado = servicio.ObtenerVentas();

            Assert.NotNull(resultado);
            Assert.Equal(3, resultado.Count);
            Assert.All(resultado, v => Assert.True(v.Total > 0));
            Assert.True(resultado[2].FechaVenta >= resultado[0].FechaVenta);
        }
    }
}
