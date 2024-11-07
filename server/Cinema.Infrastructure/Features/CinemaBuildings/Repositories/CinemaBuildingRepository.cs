using Cinema.Domain.Features.CinemaBuildings.Entities;
using Cinema.Domain.Features.CinemaBuildings.Repositories;
using Cinema.Domain.Shared.Exceptions;
using Cinema.Infrastructure.Core.Data;
using Cinema.Infrastructure.Shared.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Cinema.Infrastructure.Features.CinemaBuildings.Repositories;

internal class CinemaBuildingRepository : BaseRepository<CinemaBuilding>, ICinemaBuildingRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<CinemaBuildingRepository> _logger;

    public CinemaBuildingRepository(ApplicationDbContext dbContext, ILogger<CinemaBuildingRepository> logger
    ) : base(dbContext, logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<CinemaBuilding?> GetWithDetailsByIdAsync(int id)
    {
        try
        {
            return await _dbContext.CinemaBuilding
                .Include(cb => cb.CinemaHalls)
                .FirstOrDefaultAsync(s => s.Id == id);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while retrieving the cinema building with id {id}.",
                id);
            throw new DatabaseException($"An error occurred while retrieving the cinema building with id {id}.", e);
        }
    }
}