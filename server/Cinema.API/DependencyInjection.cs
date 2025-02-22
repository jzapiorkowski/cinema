using Cinema.API.Features.CinemaBuildings.Mappers;
using Cinema.API.Features.CinemaHalls.Mappers;
using Cinema.API.Features.Movies.Mappers;
using Cinema.API.Features.Persons.Mappers;
using Cinema.API.Features.Reservations.Mappers;
using Cinema.API.Features.Screenings.Mappers;

namespace Cinema.API;

public static class DependencyInjection
{
    public static void AddAPIServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddAutoMapper(
            typeof(CinemaBuildingProfile),
            typeof(CinemaHallProfile),
            typeof(MovieProfile),
            typeof(PersonProfile),
            typeof(ScreeningProfile),
            typeof(ReservationProfile)
        );
    }
}