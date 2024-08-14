namespace Cinema.API;

public static class DependencyInjection
{
    public static void AddAPIServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }
}