using Capa_Datos;
using Capa_Entidades;
using Microsoft.EntityFrameworkCore;


namespace PurisimoCafe.API.Data
{
    public class PurisimoCafeContext : DbContext
    {
        public PurisimoCafeContext(DbContextOptions<PurisimoCafeContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Producto> Productos { get; set; }

    }
}
