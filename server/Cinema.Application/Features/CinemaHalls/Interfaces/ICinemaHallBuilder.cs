using Cinema.Domain.Features.CinemaHalls.Entities;

namespace Cinema.Application.Features.CinemaHalls.Interfaces;

public interface ICinemaHallBuilder
{
    public ICinemaHallBuilder SetId(int id);
    public ICinemaHallBuilder SetCinemaBuildingId(int cinemaBuildingId);
    public ICinemaHallBuilder SetNumber(int number);
    public ICinemaHallBuilder SetCapacity(int capacity);
    public CinemaHall Build();
}