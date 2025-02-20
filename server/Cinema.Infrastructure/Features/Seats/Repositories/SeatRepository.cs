using System.Linq.Expressions;
using Cinema.Domain.Features.Seats.Entities;
using Cinema.Domain.Features.Seats.Repositories;
using Cinema.Infrastructure.Core.Data;
using Cinema.Infrastructure.Shared.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Cinema.Infrastructure.Features.Seats.Repositories;

internal class SeatRepository : BaseRepository<Seat>, ISeatRepository
{
    private readonly ApplicationDbContext _dbContext;

    public SeatRepository(ApplicationDbContext dbContext, ILogger<SeatRepository> logger) : base(dbContext,
        logger)
    {
        _dbContext = dbContext;
    }

    protected override IQueryable<Seat> BuildIncludesQuery(IQueryable<Seat> query)
    {
        return query
            .Include(s => s.CinemaHall)
            .Include(s => s.ReservationSeats);
    }

    public async Task<IEnumerable<Seat>> GetAllAsync(Expression<Func<Seat, bool>> predicate, bool asNoTracking = true,
        bool includeAllRelations = false)
    {
        return await ExecuteDbOperation(async () =>
        {
            IQueryable<Seat> query = _dbContext.Set<Seat>();

            if (asNoTracking)
                query = query.AsNoTracking();

            if (includeAllRelations)
                query = BuildIncludesQuery(query);

            return await query.Where(predicate).ToListAsync();
        }, "retrieving all seats with predicate");
    }
}