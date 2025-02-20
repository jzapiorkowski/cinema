using Cinema.Domain.Features.Screenings.Entities;
using Cinema.Domain.Features.Seats.Entities;

namespace Cinema.Application.Features.Screenings.Interfaces;

internal interface IScreeningService
{
    public Task<Screening> GetByIdAsync(int id);
    public Task<IEnumerable<Screening>> GetAllWithDetailsAsync(DateTime date);
    public Task<Screening> CreateAsync(Screening screening);
    public Task<Screening> UpdateAsync(Screening screening);
    public Task<bool> IsTimeSlotAvailableAsync(int cinemaHallId, DateTime startTime, DateTime endTime);
    public Task<bool> IsSeatAvailableAsync(int screeningId, int seatId);
    public Task<IEnumerable<Seat>> GetAvailableSeatsAsync(int screeningId);
}