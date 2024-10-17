using Cinema.Domain.Features.CinemaHalls.Entities;

namespace Cinema.Application.Features.CinemaHalls.Interfaces;

public interface ICinemaHallBuilder
{
    public ICinemaHallBuilder SetId(int id);
    public CinemaHall Build();
}