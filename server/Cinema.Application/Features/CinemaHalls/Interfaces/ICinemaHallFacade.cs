using Cinema.Application.Features.CinemaHalls.Dto;
using Cinema.Domain.Core.Pagination;

namespace Cinema.Application.Features.CinemaHalls.Interfaces;

public interface ICinemaHallFacade
{
    public Task<CinemaHallAppResponseDto> CreateAsync(CreateCinemaHallAppDto cinemaHall);
    public Task<CinemaHallAppResponseDto> UpdateAsync(int cinemaHallId, UpdateCinemaHallAppDto cinemaHall);
    public Task<CinemaHallWithDetailsAppResponseDto> GetByIdAsync(int cinemaHallId);
    public Task<PaginationResponse<CinemaHallAppResponseDto>> GetAllAsync(PaginationRequest paginationRequest);
    public Task DeleteAsync(int cinemaHallId);
}