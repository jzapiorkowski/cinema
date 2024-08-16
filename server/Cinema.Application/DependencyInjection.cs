using Cinema.Application.Movies.Interfaces;
using Cinema.Application.Movies.Services;
using Cinema.Application.Persons.Interfaces;
using Cinema.Application.Persons.Services;
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
        serviceCollection.AddScoped<IPersonValidationService, PersonValidationService>();
        serviceCollection.AddScoped<IPersonService, PersonService>();
    }
}