using Microsoft.Extensions.DependencyInjection;

namespace api_pos_biblioteca.Dependencias;

public static class MediadoresDependencia
{
    public static IServiceCollection AgregarMediador<T>(this IServiceCollection services)
    {
        return services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(T).Assembly));
    }
}
