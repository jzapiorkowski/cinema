using System.Linq.Expressions;
using Cinema.Domain.Features.Seats.Entities;
using Cinema.Domain.Shared.Interfaces;

namespace Cinema.Domain.Features.Seats.Repositories;

public interface ISeatRepository : IBaseRepository<Seat>
{
    Task<IEnumerable<Seat>> GetAllAsync(Expression<Func<Seat, bool>> predicate, bool asNoTracking = true,
        bool includeAllRelations = false);
}