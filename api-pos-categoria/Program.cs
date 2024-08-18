using api_pos_biblioteca.Dependencias;
using api_pos_biblioteca.Middleware;
using api_pos_categoria.Mediadores.Categorias;
using api_pos_categoria.Persistencia;
using api_pos_categoria.Servicios;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Logging.AgregarLogging();
builder.Services.AgregarReddisCache();
builder.Services.AgregarJwtToken();

builder.Services.AddTransient<ICategoriaPersistencia, CategoriaPersistencia>();
builder.Services.AddTransient<ICategoriaServicio, CategoriaServicio>();

builder.Services.AgregarMediador<ListarCategoriaRequest>();

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
