using Cinema.Application.Movies.Interfaces;
using Cinema.Application.Movies.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Cinema.Application;

public static class DependencyInjection
{
    public static void AddApplicationServices(this IServiceCollection serviceCollection)
    {
        // register automapper
        serviceCollection.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        // register services
        serviceCollection.AddScoped<IMovieService, MovieService>();
    }
}