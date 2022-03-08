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

    }
}