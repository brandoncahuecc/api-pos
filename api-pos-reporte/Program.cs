using api_pos_biblioteca.Dependencias;
using api_pos_biblioteca.Middleware;
using api_pos_reporte.Mediadores;
using api_pos_reporte.Persistencia;
using api_pos_reporte.Servicios;
using Serilog;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options => 
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Logging.AgregarLogging();
builder.Services.AgregarReddisCache();
builder.Services.AgregarJwtToken();

builder.Services.AddHttpContextAccessor();

builder.Services.AddTransient<IReportePersistencia, ReportePersistencia>();
builder.Services.AddTransient<IReporteServicio, ReporteServicio>();

builder.Services.AgregarMediador<ObtenerReporteRequest>();

var app = builder.Build();

app.UseMiddleware<CustomeMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
