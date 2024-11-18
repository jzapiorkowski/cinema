using AutoMapper;
using Cinema.Application.Features.CinemaBuildings.Dto;
using Cinema.Application.Features.CinemaBuildings.Interfaces;

namespace Cinema.Application.Features.CinemaBuildings.Facades;

internal class CinemaBuildingFacade : ICinemaBuildingFacade
{
    private readonly ICinemaBuildingService _cinemaBuildingService;
    private readonly IMapper _mapper;
    private readonly ICinemaBuildingBuilder _cinemaBuildingBuilder;

    public CinemaBuildingFacade(ICinemaBuildingService cinemaBuildingService, IMapper mapper,
        ICinemaBuildingBuilder cinemaBuildingBuilder)
    {
        _cinemaBuildingService = cinemaBuildingService;
        _mapper = mapper;
        _cinemaBuildingBuilder = cinemaBuildingBuilder;
    }

    public async Task<CinemaBuildingAppResponseDto> CreateAsync(CreateCinemaBuildingAppDto createCinemaBuildingDto)
    {
        var cinemaBuilding = _cinemaBuildingBuilder.SetAddress(createCinemaBuildingDto.Address).Build();

        var createdCinemaBuilding = await _cinemaBuildingService.CreateAsync(cinemaBuilding);
        return _mapper.Map<CinemaBuildingAppResponseDto>(createdCinemaBuilding);
    }

    public async Task<IEnumerable<CinemaBuildingAppResponseDto>> GetAllAsync()
    {
        var cinemaBuildings = await _cinemaBuildingService.GetAllAsync();
        return _mapper.Map<IEnumerable<CinemaBuildingAppResponseDto>>(cinemaBuildings);
    }

    public async Task<CinemaBuildingWithDetailsAppResponseDto> GetByIdAsync(int cinemaBuildingId)
    {
        var cinemaBuilding = await _cinemaBuildingService.GetByIdAsync(cinemaBuildingId);
        return _mapper.Map<CinemaBuildingWithDetailsAppResponseDto>(cinemaBuilding);
    }

    public async Task DeleteAsync(int id)
    {
        await _cinemaBuildingService.DeleteAsync(id);
    }
}