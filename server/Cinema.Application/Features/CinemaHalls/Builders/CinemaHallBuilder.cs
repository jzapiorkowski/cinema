using Cinema.Application.Features.CinemaHalls.Interfaces;
using Cinema.Domain.Features.CinemaHalls.Entities;

namespace Cinema.Application.Features.CinemaHalls.Builders;

public class CinemaHallBuilder : ICinemaHallBuilder
{
    private CinemaHall _cinemaHall;

    public CinemaHallBuilder()
    {
        Reset();
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

    public ICinemaHallBuilder SetNumber(int number)
    {
        _cinemaHall.Number = number;
        return this;
    }

    public ICinemaHallBuilder SetCapacity(int capacity)
    {
        _cinemaHall.Capacity = capacity;
        return this;
    }

    public CinemaHall Build()
    {
        var result = _cinemaHall;
        Reset();

        return result;
    }

    private void Reset()
    {
        _cinemaHall = new CinemaHall();
    }
}