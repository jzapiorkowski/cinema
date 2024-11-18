using Cinema.Domain.Shared.Interfaces;
using Cinema.Domain.Features.CinemaHalls.Entities;


namespace Cinema.Domain.Features.CinemaHalls.Repositories;

public interface ICinemaHallRepository : IBaseRepository<CinemaHall>
{
    public Task<CinemaHall?> GetWithDetailsByIdAsync(int id, bool asNoTracking);
}