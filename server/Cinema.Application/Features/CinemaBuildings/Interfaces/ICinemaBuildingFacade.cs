using Cinema.Application.Features.CinemaBuildings.Dto;

namespace Cinema.Application.Features.CinemaBuildings.Interfaces;

public interface ICinemaBuildingFacade
{
    public Task<CinemaBuildingAppResponseDto> CreateAsync(CreateCinemaBuildingAppDto cinemaBuilding);
    public Task<IEnumerable<CinemaBuildingAppResponseDto>> GetAllAsync();
    public Task<CinemaBuildingWithDetailsAppResponseDto> GetByIdAsync(int cinemaBuildingId);
    public Task DeleteAsync(int cinemaBuildingId);
}