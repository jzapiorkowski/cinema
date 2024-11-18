using Cinema.Application.Features.CinemaHalls.Dto;

namespace Cinema.Application.Features.CinemaHalls.Interfaces;

public interface ICinemaHallSeatFacade
{
    public Task<CinemaHallSeatAppResponseDto> CreateAsync(int cinemaHallId, CreateCinemaHallSeatAppDto seat);
    public Task<CinemaHallSeatAppResponseDto> CreateForValidatedCinemaHallAsync(int cinemaHallId, CreateCinemaHallSeatAppDto seat);
    public Task<CinemaHallSeatAppResponseDto> UpdateAsync(int cinemaHallId, int seatId, UpdateCinemaHallSeatAppDto cinemaHallSeat);
    public Task DeleteAsync(int seatId);
    public Task<CinemaHallSeatAppResponseDto> GetByIdAsync(int seatId);
}