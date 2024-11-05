using Cinema.Application.Features.CinemaHalls.Interfaces;
using Cinema.Domain.Features.CinemaHalls.Entities;

namespace Cinema.Application.Features.CinemaHalls.Builders;

public class CinemaHallBuilder : ICinemaHallBuilder
{
    private readonly CinemaHall _cinemaHall;

    public CinemaHallBuilder()
    {
        _cinemaHall = new CinemaHall();
    }

    public ICinemaHallBuilder SetId(int id)
    {
        _cinemaHall.Id = id;
        return this;
    }

    public ICinemaHallBuilder SetCinemaBuildingId(int cinemaBuildingId)
    {
        _cinemaHall.CinemaBuildingId = cinemaBuildingId;
        return this;
    }

    public CinemaHall Build()
    {
        return _cinemaHall;
    }
}