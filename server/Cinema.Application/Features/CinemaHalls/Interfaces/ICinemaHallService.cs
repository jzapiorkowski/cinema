using Cinema.Domain.Core.Pagination;
using Cinema.Domain.Features.CinemaHalls.Entities;

namespace Cinema.Application.Features.CinemaHalls.Interfaces;

internal interface ICinemaHallService
{
    public Task<CinemaHall> CreateAsync(CinemaHall cinemaHall);
    public Task<CinemaHall> UpdateAsync(CinemaHall cinemaHall);
    public Task<PaginationResponse<CinemaHall>> GetAllAsync(PaginationRequest paginationRequest);
    public Task DeleteAsync(int cinemaHallId);
    public Task<CinemaHall> GetByIdAsync(int id);
}