using Microsoft.Extensions.DependencyInjection;

namespace api_pos_biblioteca.Dependencias
{
    public static class ReddisDependecia
    {
        public static IServiceCollection AgregarReddisCache(this IServiceCollection services)
        {
            return services.AddStackExchangeRedisCache(options =>
            {
                string redisConnection = Environment.GetEnvironmentVariable("RedisConnection") ?? string.Empty;
                options.Configuration = redisConnection;
            });
        }
    }
}
