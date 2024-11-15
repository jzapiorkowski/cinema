using Cinema.Application.Features.CinemaBuildings.Builders;
using Cinema.Application.Features.CinemaBuildings.Facades;
using Cinema.Application.Features.CinemaBuildings.Interfaces;
using Cinema.Application.Features.CinemaBuildings.Services;
using Cinema.Application.Features.CinemaHalls.Builders;
using Cinema.Application.Features.CinemaHalls.Facades;
using Cinema.Application.Features.CinemaHalls.Interfaces;
using Cinema.Application.Features.CinemaHalls.Services;
using Cinema.Application.Features.CinemaHalls.Validators;
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
using Cinema.Application.Features.Screenings.Builders;
using Cinema.Application.Features.Screenings.Facades;
using Cinema.Application.Features.Screenings.Interfaces;
using Cinema.Application.Features.Screenings.Services;
using Cinema.Application.Features.Screenings.Validators;
using Cinema.Application.Features.Seats.Builders;
using Cinema.Application.Features.Seats.Interfaces;
using Cinema.Application.Features.Seats.Services;
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
        serviceCollection.AddScoped<ICinemaBuildingService, CinemaBuildingService>();
        serviceCollection.AddScoped<ISeatService, SeatService>();

        // register validators
        serviceCollection.AddTransient<IMovieRelatedEntityValidator, MovieRelatedEntityValidator>();
        serviceCollection.AddTransient<IPersonRelatedEntityValidator, PersonRelatedEntityValidator>();
        serviceCollection.AddTransient<ICinemaHallRelatedEntityValidator, CinemaHallRelatedEntityValidator>();
        serviceCollection.AddTransient<IScreeningTimeSlotValidator, ScreeningTimeSlotValidator>();
        serviceCollection.AddTransient<IScreeningValidator, ScreeningValidator>();

        // register facades
        serviceCollection.AddScoped<IMovieFacade, MovieFacade>();
        serviceCollection.AddScoped<IPersonFacade, PersonFacade>();
        serviceCollection.AddScoped<IScreeningFacade, ScreeningFacade>();
        serviceCollection.AddScoped<ICinemaHallFacade, CinemaHallFacade>();
        serviceCollection.AddScoped<ICinemaBuildingFacade, CinemaBuildingFacade>();

        // register builders
        serviceCollection.AddTransient<IMovieBuilder, MovieBuilder>();
        serviceCollection.AddTransient<IPersonBuilder, PersonBuilder>();
        serviceCollection.AddTransient<ICinemaHallBuilder, CinemaHallBuilder>();
        serviceCollection.AddTransient<IScreeningBuilder, ScreeningBuilder>();
        serviceCollection.AddTransient<ICinemaBuildingBuilder, CinemaBuildingBuilder>();
        serviceCollection.AddTransient<ISeatBuilder, SeatBuilder>();
    }
}