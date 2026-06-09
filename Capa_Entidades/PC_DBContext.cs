using Microsoft.EntityFrameworkCore;

namespace Capa_Entidades
{
    public class PC_DBContext : DbContext
    {
        public PC_DBContext(DbContextOptions<PC_DBContext> options) : base(options) { }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
