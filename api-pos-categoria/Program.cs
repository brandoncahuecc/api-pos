using api_pos_categoria.Mediadores;
using api_pos_categoria.Middleware;
using api_pos_categoria.Persistencia;
using api_pos_categoria.Servicios;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var loggerConfi = new LoggerConfiguration()
    .ReadFrom.Configuration(new ConfigurationBuilder().AddJsonFile("./Recursos/serilog-config.json").Build())
    .Enrich.FromLogContext().CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(loggerConfi);

builder.Services.AddStackExchangeRedisCache(options =>
{
    string redisConnection = Environment.GetEnvironmentVariable("RedisConnection") ?? string.Empty;
    options.Configuration = redisConnection;
});
builder.Services.AddTransient<ICategoriaPersistencia, CategoriaPersistencia>();
builder.Services.AddTransient<ICategoriaServicio, CategoriaServicio>();
builder.Services.AgregarMediador();

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
