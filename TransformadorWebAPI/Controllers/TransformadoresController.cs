using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Features;
using NetTopologySuite.IO;
using TransformadorWebAPI.Data;

[ApiController]
[Route("api/transformadores")]
public class TransformadoresController : ControllerBase
{
    private readonly AppDbContext _context;

    public TransformadoresController(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Obtiene transformadores en formato GeoJSON
    /// </summary>
    /// <param name="estado">DISPONIBLE | CONDICIONADA | CRITICA</param>
    /// <returns>FeatureCollection GeoJSON</returns>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet]
    public IActionResult Get([FromQuery] string? estado, [FromQuery] string? codigo)
    {
        try
        {
            var query = _context.Transformadores.AsQueryable();

            if (!string.IsNullOrEmpty(estado))
                query = query.Where(t => t.Estado == estado);

            if (!string.IsNullOrWhiteSpace(codigo))
                query = query.Where(t => t.Codigo == codigo);

            var features = query
                .Select(t => new Feature(
                    t.Ubicacion,
                    new AttributesTable
                    {
                { "id", t.Id },
                { "estado", t.Estado },
                { "codigo", t.Codigo }
                    }
                ))
                .ToList();

            var collection = new FeatureCollection();
            features.ForEach(collection.Add);

            var geoJson = new GeoJsonWriter().Write(collection);

            // ⬇️ CLAVE: devolver string como JSON
            return Content(geoJson, "application/json");
        }
        catch (Exception ex)
        {
            // Esto te ayudará a ver el error real en la consola de Visual Studio / Terminal
            Console.WriteLine($"Error: {ex.Message}");
            return StatusCode(500, "Error interno al procesar GeoJSON");
        }
    }
}
