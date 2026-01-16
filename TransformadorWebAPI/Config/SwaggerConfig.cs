using Microsoft.OpenApi.Models;

namespace TransformadorWebAPI.Config;

public static class SwaggerConfig
{
    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Transformadores GIS API",
                Version = "v1",
                Description = "API para visualización geoespacial de transformadores (PostGIS)",
                Contact = new OpenApiContact
                {
                    Name = "Equipo GIS",
                    Email = "gis@empresa.com"
                }
            });
        });
    }
}
