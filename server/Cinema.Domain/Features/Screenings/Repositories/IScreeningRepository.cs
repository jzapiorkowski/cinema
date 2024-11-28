using Cinema.Domain.Features.Screenings.Entities;
using Cinema.Domain.Shared.Interfaces;

namespace Cinema.Domain.Features.Screenings.Repositories;

public interface IScreeningRepository : IBaseRepository<Screening>
{
    public Task<IEnumerable<Screening>> GetAllWithDetailsAsync(DateTime date);
    public Task<bool> IsTimeSlotAvailableAsync(int cinemaHallId, DateTime startTime, DateTime endTime);
}