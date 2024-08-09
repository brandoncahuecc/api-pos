using api_pos_categoria.Mediadores;
using api_pos_categoria.Persistencia;
using api_pos_categoria.Servicios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddStackExchangeRedisCache(options =>
{
    string redisConnection = Environment.GetEnvironmentVariable("RedisConnection") ?? string.Empty;
    options.Configuration = redisConnection;
});
builder.Services.AddTransient<ICategoriaPersistencia, CategoriaPersistencia>();
builder.Services.AddTransient<ICategoriaServicio, CategoriaServicio>();
builder.Services.AgregarMediador();

var app = builder.Build();

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
