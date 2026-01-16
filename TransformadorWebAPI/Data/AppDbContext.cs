using Microsoft.EntityFrameworkCore;
using TransformadorWebAPI.Models;

namespace TransformadorWebAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Transformador> Transformadores => Set<Transformador>();
    }
}
