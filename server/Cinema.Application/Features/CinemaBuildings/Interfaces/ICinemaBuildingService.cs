using Cinema.Domain.Features.CinemaBuildings.Entities;

namespace Cinema.Application.Features.CinemaBuildings.Interfaces;

internal interface ICinemaBuildingService
{
    public Task<CinemaBuilding> CreateAsync(CinemaBuilding cinemaBuilding);
    public Task<IEnumerable<CinemaBuilding>> GetAllAsync();
    public Task<CinemaBuilding> GetByIdWithDetailsAsync(int cinemaBuildingId);
    public Task DeleteAsync(int cinemaBuildingId);
}