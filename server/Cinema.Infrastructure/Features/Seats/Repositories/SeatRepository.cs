using Cinema.Domain.Features.Seats.Entities;
using Cinema.Domain.Features.Seats.Repositories;
using Cinema.Domain.Shared.Exceptions;
using Cinema.Infrastructure.Core.Data;
using Cinema.Infrastructure.Shared.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Cinema.Infrastructure.Features.Seats.Repositories;

internal class SeatRepository : BaseRepository<Seat>, ISeatRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<SeatRepository> _logger;
    
    public SeatRepository(ApplicationDbContext dbContext, ILogger<SeatRepository> logger) : base(dbContext,
        logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Seat?> GetWithDetailsByIdAsync(int id, bool asNoTracking)
    {
        try
        {
            IQueryable<Seat> query = _dbContext.Seat;

            if (asNoTracking)
                query = query.AsNoTracking();
            
            return await query
                .Include(s => s.CinemaHall)
                .FirstOrDefaultAsync(s => s.Id == id);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while retrieving the seat with id {id}.",
                id);
            throw new DatabaseException($"An error occurred while retrieving the seat with id {id}.", e);
        }
    }
}