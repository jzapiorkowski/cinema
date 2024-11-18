using Cinema.Domain.Features.Seats.Entities;
using Cinema.Domain.Shared.Interfaces;

namespace Cinema.Domain.Features.Seats.Repositories;

public interface ISeatRepository : IBaseRepository<Seat>
{
    public Task<Seat?> GetWithDetailsByIdAsync(int id, bool asNoTracking);
}