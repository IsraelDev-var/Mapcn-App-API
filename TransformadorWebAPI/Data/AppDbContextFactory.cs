using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using TransformadorWebAPI.Data;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        var connectionString =
            "Host=centerbeam.proxy.rlwy.net;Port=37138;Database=railway;Username=postgres;Password=lLIJbocPEGOKTkrhFnDHasnVcKnWLjrH";

        optionsBuilder.UseNpgsql(connectionString,
            o => o.UseNetTopologySuite());

        return new AppDbContext(optionsBuilder.Options);
    }
}
