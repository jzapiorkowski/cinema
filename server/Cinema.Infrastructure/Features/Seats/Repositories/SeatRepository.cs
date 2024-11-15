using Cinema.Domain.Features.Seats.Entities;
using Cinema.Domain.Features.Seats.Repositories;
using Cinema.Infrastructure.Core.Data;
using Cinema.Infrastructure.Shared.Repositories;
using Microsoft.Extensions.Logging;

namespace Cinema.Infrastructure.Features.Seats.Repositories;

internal class SeatRepository : BaseRepository<Seat>, ISeatRepository
{
    public SeatRepository(ApplicationDbContext dbContext, ILogger<BaseRepository<Seat>> logger) : base(dbContext,
        logger)
    {
    }
}