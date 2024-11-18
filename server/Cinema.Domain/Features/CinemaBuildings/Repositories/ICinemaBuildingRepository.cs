using Cinema.Domain.Features.CinemaBuildings.Entities;
using Cinema.Domain.Shared.Interfaces;

namespace Cinema.Domain.Features.CinemaBuildings.Repositories;

public interface ICinemaBuildingRepository : IBaseRepository<CinemaBuilding>
{
    public Task<CinemaBuilding?> GetWithDetailsByIdAsync(int id, bool asNoTracking);
}