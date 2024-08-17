using Cinema.Application.Features.Movies.Builders;
using Cinema.Application.Features.Movies.Facades;
using Cinema.Application.Features.Movies.Interfaces;
using Cinema.Application.Features.Movies.Services;
using Cinema.Application.Features.Persons.Facades;
using Cinema.Application.Features.Persons.Interfaces;
using Cinema.Application.Features.Persons.Services;
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
        
        // register facades
        serviceCollection.AddScoped<IMovieFacade, MovieFacade>();
        serviceCollection.AddScoped<IPersonFacade, PersonFacade>();
        
        // register builders
        serviceCollection.AddTransient<IMovieBuilder, MovieBuilder>();
    }
}