using Microsoft.EntityFrameworkCore;
using MecaControlRD.Data;
using MecaControlRD.Services;

var builder = WebApplication.CreateBuilder(args);

// Controladores
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler =
            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

// OpenAPI y Swagger
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Base de datos SQL Server
builder.Services.AddDbContextFactory<MecaControlDbContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

// Servicios
builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<VehiculoService>();
builder.Services.AddScoped<ServicioService>();
builder.Services.AddScoped<TrabajoService>();
builder.Services.AddScoped<RecordatorioService>();
builder.Services.AddScoped<PagoService>();
builder.Services.AddScoped<UsuarioService>();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("BlazorPolicy", policy =>
    {
        policy
            .WithOrigins("https://localhost:7080")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configuración HTTP
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("BlazorPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
