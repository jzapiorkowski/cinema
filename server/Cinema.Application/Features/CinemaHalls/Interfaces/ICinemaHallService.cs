using Cinema.Domain.Features.CinemaHalls.Entities;

namespace Cinema.Application.Features.CinemaHalls.Interfaces;

internal interface ICinemaHallService
{
    public Task<CinemaHall> CreateAsync(CinemaHall cinemaHall);
    public Task<CinemaHall> GetByIdWithDetailsAsync(int cinemaHallId);
    public Task<IEnumerable<CinemaHall>> GetAllAsync();
    public Task DeleteAsync(int cinemaHallId);
    public Task<CinemaHall> GetByIdAsync(int cinemaHallId);
}