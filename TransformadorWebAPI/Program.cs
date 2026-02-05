using Microsoft.EntityFrameworkCore;
using TransformadorWebAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Cadena de conexión:
// - Local: appsettings.json
// - Producción (Railway): variable de entorno
var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection")
    ?? builder.Configuration["DATABASE_URL"];

connectionString = connectionString.Trim();
if (connectionString.StartsWith("postgresql://"))
{
    var uri = new Uri(connectionString);
    var userInfo = uri.UserInfo.Split(':');

    connectionString =
        $"Host={uri.Host};Port={uri.Port};Database={uri.AbsolutePath.Trim('/')};" +
        $"Username={userInfo[0]};Password={userInfo[1]};Ssl Mode=Require;Trust Server Certificate=true";
}


// Si no existe (por ejemplo en Railway Preview)
if (string.IsNullOrEmpty(connectionString))
{
    connectionString =
        "Host=centerbeam.proxy.rlwy.net;Port=37138;Database=railway;Username=postgres;Password=lLIJbocPEGOKTkrhFnDHasnVcKnWLjrH";
}

// DbContext con PostGIS
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString,
        o => o.UseNetTopologySuite())
);

builder.Services.AddControllers()
    .AddNewtonsoftJson();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS para Next.js
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");
app.UseAuthorization();

app.MapControllers();

app.Run();
