using Cinema.Domain.Core.Interfaces;
using Cinema.Domain.Features.CinemaBuildings.Repositories;
using Cinema.Domain.Features.CinemaHalls.Repositories;
using Cinema.Domain.Features.Movies.Interfaces;
using Cinema.Domain.Features.Persons.Repositories;
using Cinema.Domain.Features.Reservations.Repositories;
using Cinema.Domain.Features.ReservationsSeats.Repositories;
using Cinema.Domain.Features.Screenings.Repositories;
using Cinema.Domain.Features.Seats.Repositories;
using Cinema.Domain.Features.Tickets.Repositories;
using Cinema.Domain.Shared.Interfaces;
using Cinema.Infrastructure.Core.Configs;
using Cinema.Infrastructure.Core.Data;
using Cinema.Infrastructure.Features.CinemaBuildings.Repositories;
using Cinema.Infrastructure.Features.CinemaHalls.Repositories;
using Cinema.Infrastructure.Features.Movies.Repositories;
using Cinema.Infrastructure.Features.Persons.Repositories;
using Cinema.Infrastructure.Features.Reservations.Repositories;
using Cinema.Infrastructure.Features.ReservationSeats.Repositories;
using Cinema.Infrastructure.Features.Screenings.Repositories;
using Cinema.Infrastructure.Features.Seats.Repositories;
using Cinema.Infrastructure.Features.Tickets.Repositories;
using Cinema.Infrastructure.Shared.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace Cinema.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructureServices(this IServiceCollection serviceCollection)
    {
        // register db context
        serviceCollection.AddDbContext<ApplicationDbContext>();

        // register unit of work
        serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
        serviceCollection.AddScoped<ITransactionalUnitOfWork, TransactionalUnitOfWork>();

        // register configurations
        serviceCollection.AddSingleton<IAppConfiguration, AppConfiguration>();

        // register repositories
        serviceCollection.AddScoped<IMovieRepository, MovieRepository>();
        serviceCollection.AddScoped<IPersonRepository, PersonRepository>();
        serviceCollection.AddScoped<IScreeningRepository, ScreeningRepository>();
        serviceCollection.AddScoped<ICinemaHallRepository, CinemaHallRepository>();
        serviceCollection.AddScoped<ICinemaBuildingRepository, CinemaBuildingRepository>();
        serviceCollection.AddScoped<ISeatRepository, SeatRepository>();
        serviceCollection.AddScoped<IReservationRepository, ReservationRepository>();
        serviceCollection.AddScoped<IReservationSeatRepository, ReservationSeatRepository>();
        serviceCollection.AddScoped<ITicketRepository, TicketRepository>();
    }
}