using AutoMapper;
using Cinema.Application.Features.CinemaHalls.Dto;
using Cinema.Application.Features.CinemaHalls.Interfaces;

namespace Cinema.Application.Features.CinemaHalls.Facades;

internal class CinemaHallFacade : ICinemaHallFacade
{
    private readonly ICinemaHallService _cinemaHallService;
    private readonly IMapper _mapper;
    private readonly ICinemaHallBuilder _cinemaHallBuilder;

    public CinemaHallFacade(ICinemaHallService cinemaHallService, IMapper mapper, ICinemaHallBuilder cinemaHallBuilder)
    {
        _cinemaHallService = cinemaHallService;
        _mapper = mapper;
        _cinemaHallBuilder = cinemaHallBuilder;
    }

    public async Task<IEnumerable<CinemaHallAppResponseDto>> GetAllAsync()
    {
        var cinemaHalls = await _cinemaHallService.GetAllAsync();
        return _mapper.Map<IEnumerable<CinemaHallAppResponseDto>>(cinemaHalls);
    }

    public async Task<CinemaHallWithDetailsAppResponseDto> GetByIdWithDetailsAsync(int id)
    {
        var person = await _cinemaHallService.GetByIdWithDetailsAsync(id);
        return _mapper.Map<CinemaHallWithDetailsAppResponseDto>(person);
    }

    public async Task DeleteAsync(int id)
    {
        await _cinemaHallService.DeleteAsync(id);
    }

    public async Task<CinemaHallAppResponseDto> CreateAsync(CreateCinemaHallAppDto createCinemaHallAppDto)
    {
        var cinemaHall = _cinemaHallBuilder.SetCinemaBuildingId(createCinemaHallAppDto.CinemaBuildingId).Build();

        var createdCinemaHall = await _cinemaHallService.CreateAsync(cinemaHall);
        return _mapper.Map<CinemaHallAppResponseDto>(createdCinemaHall);
    }
}