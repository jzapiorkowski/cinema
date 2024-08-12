namespace Cinema.API;

public static class DependencyInjection
{
    public static void AddCinemaServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }
}