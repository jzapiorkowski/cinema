using Cinema.Application.Features.CinemaHalls.Facades;
using Cinema.Application.Features.CinemaHalls.Interfaces;
using Cinema.Application.Features.CinemaHalls.Services;
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
using Cinema.Application.Features.Screenings.Facades;
using Cinema.Application.Features.Screenings.Interfaces;
using Cinema.Application.Features.Screenings.Services;
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
        serviceCollection.AddScoped<IScreeningService, ScreeningService>();
        serviceCollection.AddScoped<ICinemaHallService, CinemaHallService>();
        
        // register validators
        serviceCollection.AddTransient<IMovieRelatedEntityValidator, MovieRelatedEntityValidator>();
        serviceCollection.AddTransient<IPersonRelatedEntityValidator, PersonRelatedEntityValidator>();
        
        // register facades
        serviceCollection.AddScoped<IMovieFacade, MovieFacade>();
        serviceCollection.AddScoped<IPersonFacade, PersonFacade>();
        serviceCollection.AddScoped<IScreeningFacade, ScreeningFacade>();
        serviceCollection.AddScoped<ICinemaHallFacade, CinemaHallFacade>();
        
        // register builders
        serviceCollection.AddTransient<IMovieBuilder, MovieBuilder>();
        serviceCollection.AddTransient<IPersonBuilder, PersonBuilder>();
    }
}