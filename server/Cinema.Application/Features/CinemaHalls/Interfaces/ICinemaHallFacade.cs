using Cinema.Application.Features.CinemaHalls.Dto;
using Cinema.Domain.Features.CinemaHalls.Entities;

namespace Cinema.Application.Features.CinemaHalls.Interfaces;

public interface ICinemaHallFacade
{
    // public Task<CinemaHallAppResponseDto> CreateAsync(CinemaHall cinemaHall);
    public Task<CinemaHallAppResponseDto> GetByIdAWithDetailsAsync(int cinemaHallId);
    public Task<IEnumerable<CinemaHallAppResponseDto>> GetAllAsync();
    public Task DeleteAsync(int cinemaHallId);
    // public Task<CinemaHallAppResponseDto> UpdateAsync(CinemaHall cinemaHall);
}