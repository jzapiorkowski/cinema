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


    public async Task<Screening?> GetWithDetailsByIdAsync(int id)
    {
        try
        {
            return await _dbContext.Screening
                .Include(s => s.Movie)
                .FirstOrDefaultAsync(s => s.Id == id);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while retrieving the screening with id {id}.",
                id);
            throw new DatabaseException($"An error occurred while retrieving the screening with id {id}.", e);
        }
    }
}