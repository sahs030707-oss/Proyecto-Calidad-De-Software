// PU-007 — Hash de Contraseña con Argon2id
using Capa_Datos;
using Capa_Entidades;
using Capa_Negocio;
using Microsoft.Extensions.Configuration;
using Moq;

namespace PurisimoCafe.Tests
{
    public class UsuarioServicioTests
    {
        [Fact]
        public void AddUsuario_ConDatosValidos_HashearClave_LlamaInsertar()
        {
            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "ConnectionStrings:DefaultConnection", "Server=.;" }
                })
                .Build();

            var mockDatos = new Mock<UsuarioDatos>(config);
            Usuario? usuGuardado = null;

            mockDatos.Setup(d => d.Insertar(It.IsAny<Usuario>()))
                     .Callback<Usuario>(u => usuGuardado = u);

            var servicio = new UsuarioServicio(mockDatos.Object);
            var usuario = new Usuario
            {
                Nombre = "María López",
                Correo = "maria@cafe.com",
                RolID = 2
            };

            const string claveOriginal = "MiClave2026!";

            servicio.AddUsuario(usuario, claveOriginal);

            Assert.NotNull(usuGuardado);
            Assert.NotEqual(claveOriginal, usuGuardado!.Contraseña);
            Assert.Contains(".", usuGuardado.Contraseña);
            Assert.True(usuGuardado.Contraseña.Length > 20);
            mockDatos.Verify(d => d.Insertar(It.IsAny<Usuario>()), Times.Once);
        }
    }
}
