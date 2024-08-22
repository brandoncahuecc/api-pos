using api_pos_articulo.Mediadores;
using api_pos_articulo.Persistencia;
using api_pos_articulo.Servicios;
using api_pos_biblioteca.Dependencias;
using api_pos_biblioteca.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Logging.AgregarLogging();
builder.Services.AgregarReddisCache();
builder.Services.AgregarJwtToken();

builder.Services.AddHttpContextAccessor();

builder.Services.AddTransient<IArticuloPersistencia, ArticuloPersistencia>();
builder.Services.AddTransient<IArticuloServicio, ArticuloServicio>();

builder.Services.AgregarMediador<ListarArticulosRequest>();

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
