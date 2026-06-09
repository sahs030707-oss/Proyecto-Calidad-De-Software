// PU-009 & PU-010 — Listados de Usuarios y Categorías
using Capa_Datos;
using Capa_Entidades;
using Capa_Negocio;
using Moq;

namespace PurisimoCafe.Tests
{
    public class ListadosTests
    {
        [Fact]
        public void GetUsuarios_RetornaListaCompleta()
        {
            var mockDatos = new Mock<IUsuarioDatos>();
            mockDatos.Setup(d => d.ObtenerUsuarios())
                     .Returns(new List<Usuario>
                     {
                         new Usuario { UsuarioID = 1, Nombre = "Admin", Correo = "admin@cafe.com" },
                         new Usuario { UsuarioID = 2, Nombre = "Cajero", Correo = "cajero@cafe.com" }
                     });

            var servicio = new UsuarioServicio(mockDatos.Object);
            var resultado = servicio.GetUsuarios();

            Assert.NotNull(resultado);
            var lista = new List<Usuario>(resultado);
            Assert.Equal(2, lista.Count);
            Assert.Equal("Admin", lista[0].Nombre);
            Assert.Equal("Cajero", lista[1].Nombre);
        }

        [Fact]
        public void ObtenerCategorias_RetornaListaNoVacia()
        {
            var mockDatos = new Mock<ICategoriaDatos>();
            mockDatos.Setup(d => d.ObtenerCategorias())
                     .Returns(new List<Categorias>
                     {
                         new Categorias { CategoriaID = 1, Nombre = "Café" },
                         new Categorias { CategoriaID = 2, Nombre = "Té" },
                         new Categorias { CategoriaID = 3, Nombre = "Postres" }
                     });

            var servicio = new CategoriaServicio(mockDatos.Object);
            var resultado = servicio.ObtenerCategorias();

            Assert.NotNull(resultado);
            Assert.Equal(3, resultado.Count);
            Assert.All(resultado, c => Assert.False(string.IsNullOrWhiteSpace(c.Nombre)));
        }
    }
}
