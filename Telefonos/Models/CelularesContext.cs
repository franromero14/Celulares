using Microsoft.EntityFrameworkCore;
using Celulares.Models;

namespace Telefonos.Models
{
    public class CelularesContext : DbContext
    {
        public CelularesContext(DbContextOptions<CelularesContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        public DbSet<Celulares.Models.App> App { get; set; }

        public DbSet<Celulares.Models.Instalacion> Instalacion { get; set; }

        public DbSet<Celulares.Models.Operario> Operario { get; set; }

        public DbSet<Celulares.Models.Sensor> Sensor { get; set; }

        public DbSet<Celulares.Models.Telefono> Telefono { get; set; }

    }
}