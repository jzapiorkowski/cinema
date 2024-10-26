using Cinema.Application.Features.CinemaHalls.Dto;

namespace Cinema.Application.Features.CinemaHalls.Interfaces;

public interface ICinemaHallFacade
{
    public Task<CinemaHallAppResponseDto> CreateAsync(CreateCinemaHallAppDto cinemaHall);
    public Task<CinemaHallWithDetailsAppResponseDto> GetByIdWithDetailsAsync(int cinemaHallId);
    public Task<IEnumerable<CinemaHallAppResponseDto>> GetAllAsync();
    public Task DeleteAsync(int cinemaHallId);
}