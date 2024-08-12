using Cinema.Domain.Interfaces;
using Cinema.Infrastructure.Configs;
using Cinema.Infrastructure.Data;
using Cinema.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Cinema.Infrastructure;

public static class DependencyInjection
{
    public static void AddCinemaServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContext<ApplicationDbContext>();
        
        serviceCollection.AddScoped<IMovieRepository, MovieRepository>();
        
        serviceCollection.AddSingleton<IAppConfiguration, AppConfiguration>();
    }
}