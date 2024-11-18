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


    public async Task<Screening?> GetWithDetailsByIdAsync(int id, bool asNoTracking)
    {
        try
        {
            IQueryable<Screening> query = _dbContext.Screening;

            if (asNoTracking)
                query = query.AsNoTracking();
            
            return await query
                .Include(s => s.Movie)
                .Include(s => s.CinemaHall)
                .FirstOrDefaultAsync(s => s.Id == id);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while retrieving the screening with id {id}.",
                id);
            throw new DatabaseException($"An error occurred while retrieving the screening with id {id}.", e);
        }
    }

    public async Task<IEnumerable<Screening>> GetAllWithDetailsAsync(DateTime date)
    {
        try
        {
            return await _dbContext.Screening.AsNoTracking().Where(s => s.StartTime.Date == date.Date)
                .Include(s => s.Movie).Include(s => s.CinemaHall).ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while retrieving all screenings.");
            throw new DatabaseException($"An error occurred while retrieving all screenings.", e);
        }
    }

    public async Task<bool> IsTimeSlotAvailableAsync(int cinemaHallId, DateTime startTime, DateTime endTime)
    {
        try
        {
            return !await _dbContext.Screening
                .Where(s => s.CinemaHallId == cinemaHallId)
                .AnyAsync(s =>
                    s.StartTime < endTime && s.StartTime.AddMinutes(s.Movie.Duration.TotalMinutes) > startTime);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while checking if screening time slot is available.");
            throw new DatabaseException($"An error occurred while checking if screening time slot is available.", e);
        }
    }
}