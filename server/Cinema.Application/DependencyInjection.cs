using Cinema.Application.Features.Movies.Builders;
using Cinema.Application.Features.Movies.Facades;
using Cinema.Application.Features.Movies.Interfaces;
using Cinema.Application.Features.Movies.Services;
using Cinema.Application.Features.Movies.Validators;
using Cinema.Application.Features.Persons.Builders;
using Cinema.Application.Features.Persons.Facades;
using Cinema.Application.Features.Persons.Interfaces;
using Cinema.Application.Features.Persons.Services;
using Cinema.Application.Features.Persons.Validators;
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
        serviceCollection.AddScoped<IPersonService, PersonService>();
        
        // register validators
        serviceCollection.AddTransient<IMovieRelatedEntityValidator, MovieRelatedEntityValidator>();
        serviceCollection.AddTransient<IPersonRelatedEntityValidator, PersonRelatedEntityValidator>();
        
        // register facades
        serviceCollection.AddScoped<IMovieFacade, MovieFacade>();
        serviceCollection.AddScoped<IPersonFacade, PersonFacade>();
        
        // register builders
        serviceCollection.AddTransient<IMovieBuilder, MovieBuilder>();
        serviceCollection.AddTransient<IPersonBuilder, PersonBuilder>();
    }
}