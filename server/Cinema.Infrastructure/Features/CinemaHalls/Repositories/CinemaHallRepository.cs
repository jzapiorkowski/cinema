using Cinema.Domain.Features.CinemaHalls.Entities;
using Cinema.Domain.Features.CinemaHalls.Repositories;
using Cinema.Domain.Shared.Exceptions;
using Cinema.Infrastructure.Core.Data;
using Cinema.Infrastructure.Shared.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Cinema.Infrastructure.Features.CinemaHalls.Repositories;

internal class CinemaHallRepository : BaseRepository<CinemaHall>, ICinemaHallRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<CinemaHallRepository> _logger;

    public CinemaHallRepository(ApplicationDbContext dbContext, ILogger<CinemaHallRepository> logger
    ) : base(dbContext, logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }


    public async Task<CinemaHall?> GetWithDetailsByIdAsync(int id)
    {
        try
        {
            return await _dbContext.CinemaHall
                .Include(ch => ch.Screenings)
                .FirstOrDefaultAsync(s => s.Id == id);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while retrieving the cinema hall with id {id}.",
                id);
            throw new DatabaseException($"An error occurred while retrieving the cinema hall with id {id}.", e);
        }
    }
}