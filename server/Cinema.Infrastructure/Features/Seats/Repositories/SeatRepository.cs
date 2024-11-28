using Cinema.Domain.Features.Seats.Entities;
using Cinema.Domain.Features.Seats.Repositories;
using Cinema.Infrastructure.Core.Data;
using Cinema.Infrastructure.Shared.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Cinema.Infrastructure.Features.Seats.Repositories;

internal class SeatRepository : BaseRepository<Seat>, ISeatRepository
{
    
    public SeatRepository(ApplicationDbContext dbContext, ILogger<SeatRepository> logger) : base(dbContext,
        logger)
    {
    }

    protected override IQueryable<Seat> BuildIncludesQuery(IQueryable<Seat> query)
    {
        return query
            .Include(s => s.CinemaHall);
    }
}