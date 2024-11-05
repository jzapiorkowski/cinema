using Cinema.Domain.Features.CinemaBuildings.Entities;
using Cinema.Domain.Features.CinemaBuildings.Repositories;
using Cinema.Infrastructure.Core.Data;
using Cinema.Infrastructure.Shared.Repositories;
using Microsoft.Extensions.Logging;

namespace Cinema.Infrastructure.Features.CinemaBuildings.Repositories;

internal class CinemaBuildingRepository : BaseRepository<CinemaBuilding>, ICinemaBuildingRepository
{
    public CinemaBuildingRepository(ApplicationDbContext dbContext, ILogger<CinemaBuildingRepository> logger
    ) : base(dbContext, logger)
    {
    }
}