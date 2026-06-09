// PU-001 & PU-002 — Pruebas de Login
using Capa_Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PurisimoCafe.API.Controllers;

namespace PurisimoCafe.Tests
{
    public class LoginControllerTests
    {
        private PC_DBContext CreateContext()
        {
            var opts = new DbContextOptionsBuilder<PC_DBContext>()
                .UseInMemoryDatabase("LoginTestDb_" + Guid.NewGuid())
                .Options;

            var ctx = new PC_DBContext(opts);
            ctx.Usuarios.Add(new Usuario
            {
                UsuarioID = 1,
                Nombre = "Admin",
                Correo = "admin@cafe.com",
                Contraseña = "clave123",
                RolID = 1
            });
            ctx.SaveChanges();
            return ctx;
        }

        private IConfiguration CreateConfig()
        {
            return new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "Jwt:Key", "PurisimoCafeSecretKeyForTesting2026!" },
                    { "Jwt:Issuer", "PurisimoCafe" },
                    { "Jwt:Audience", "PurisimoCafeUsers" }
                })
                .Build();
        }

        [Fact]
        public void Login_ConCredencialesValidas_RetornaOk()
        {
            var ctx = CreateContext();
            var config = CreateConfig();
            var controller = new LoginController(ctx, config);

            var request = new LoginRequest
            {
                Correo = "admin@cafe.com",
                Clave = "clave123"
            };

            var result = controller.Login(request);

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.True((ok.StatusCode ?? 200) == 200);

            var json = JsonSerializer.Serialize(ok.Value);
            Assert.Contains("Token", json, StringComparison.OrdinalIgnoreCase);
            Assert.Contains("admin@cafe.com", json, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Login_ConClaveIncorrecta_RetornaUnauthorized()
        {
            var ctx = CreateContext();
            var config = CreateConfig();
            var controller = new LoginController(ctx, config);

            var request = new LoginRequest
            {
                Correo = "admin@cafe.com",
                Clave = "claveErronea"
            };

            var result = controller.Login(request);

            Assert.IsType<UnauthorizedObjectResult>(result);
        }
    }
}