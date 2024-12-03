using Cinema.Application.Features.CinemaBuildings.Dto;
using Cinema.Domain.Core.Pagination;

namespace Cinema.Application.Features.CinemaBuildings.Interfaces;

public interface ICinemaBuildingFacade
{
    public Task<CinemaBuildingAppResponseDto> CreateAsync(CreateCinemaBuildingAppDto cinemaBuilding);
    public Task<PaginationResponse<CinemaBuildingAppResponseDto>> GetAllAsync(PaginationRequest paginationRequest);
    public Task<CinemaBuildingWithDetailsAppResponseDto> GetByIdAsync(int cinemaBuildingId);
    public Task DeleteAsync(int cinemaBuildingId);
}