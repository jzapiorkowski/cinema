using Cinema.Application.Features.Screenings.Dto;
using Cinema.Domain.Features.Seats.Entities;

namespace Cinema.Application.Features.Screenings.Interfaces;

public interface IScreeningFacade
{
    public Task<ScreeningWithDetailsAppResponseDto> GetByIdAsync(int id);
    public Task<IEnumerable<ScreeningWithDetailsAppResponseDto>> GetAllWithDetailsAsync(DateTime date);
    public Task<ScreeningAppResponseDto> CreateAsync(CreateScreeningAppDto screening);
    public Task<ScreeningAppResponseDto> UpdateAsync(int id, UpdateScreeningAppDto screening);
    Task<IEnumerable<Seat>> GetAvailableSeatsAsync(int screeningId);
}