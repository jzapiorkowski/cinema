using Cinema.Domain.Features.Screenings.Entities;
using Cinema.Domain.Features.Screenings.Repositories;
using Cinema.Domain.Shared.Exceptions;
using Cinema.Infrastructure.Core.Data;
using Cinema.Infrastructure.Shared.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Cinema.Infrastructure.Features.Screenings.Repositories;

internal class ScreeningRepository : BaseRepository<Screening>, IScreeningRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<ScreeningRepository> _logger;

    public ScreeningRepository(ApplicationDbContext dbContext, ILogger<ScreeningRepository> logger
    ) : base(dbContext, logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<IEnumerable<Screening>> GetAllWithDetailsAsync(DateTime date)
    {
        return await ExecuteDbOperation(async () =>
        {
            return await _dbContext.Screening.AsNoTracking().Where(s => s.StartTime.Date == date.Date)
                .Include(s => s.Movie).Include(s => s.CinemaHall).ToListAsync();
        }, "retrieving all screenings with details");
    }

    public async Task<bool> IsTimeSlotAvailableAsync(int cinemaHallId, DateTime startTime, DateTime endTime)
    {
        return await ExecuteDbOperation(async () =>
        {
            return !await _dbContext.Screening
                .Where(s => s.CinemaHallId == cinemaHallId)
                .AnyAsync(s =>
                    s.StartTime < endTime && s.StartTime.AddMinutes(s.Movie.Duration.TotalMinutes) > startTime);
        }, "checking if screening time slot is available");
    }

    protected override IQueryable<Screening> BuildIncludesQuery(IQueryable<Screening> query)
    {
        return query
            .Include(s => s.Movie)
            .Include(s => s.CinemaHall);
    }
}