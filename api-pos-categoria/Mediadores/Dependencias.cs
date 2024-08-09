namespace api_pos_categoria.Mediadores;

public static class Dependencias
{
    public static IServiceCollection AgregarMediador(this IServiceCollection services)
    {
        return services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(Dependencias).Assembly));
    }
}
