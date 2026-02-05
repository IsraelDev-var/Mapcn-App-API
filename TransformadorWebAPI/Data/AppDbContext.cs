using Microsoft.EntityFrameworkCore;
using TransformadorWebAPI.Models;

namespace TransformadorWebAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Transformador> Transformadores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Esto asegura que las funciones de NetTopologySuite se activen en la DB
            modelBuilder.HasPostgresExtension("postgis");
            base.OnModelCreating(modelBuilder);
        }
    }
}