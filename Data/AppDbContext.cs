using Microsoft.EntityFrameworkCore;
using Parcial2DDA.Models;

namespace Parcial2DDA.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        
        public DbSet<Medicion> Mediciones { get; set; }
    }
}
