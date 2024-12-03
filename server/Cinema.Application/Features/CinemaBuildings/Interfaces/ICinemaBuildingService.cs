using Cinema.Domain.Core.Pagination;
using Cinema.Domain.Features.CinemaBuildings.Entities;

namespace Cinema.Application.Features.CinemaBuildings.Interfaces;

internal interface ICinemaBuildingService
{
    public Task<CinemaBuilding> CreateAsync(CinemaBuilding cinemaBuilding);
    public Task<PaginationResponse<CinemaBuilding>> GetAllAsync(PaginationRequest paginationRequest);
    public Task<CinemaBuilding?> GetByIdAsync(int cinemaBuildingId);
    public Task DeleteAsync(int cinemaBuildingId);
}