using AutoMapper;
using Cinema.Application.Features.CinemaHalls.Dto;
using Cinema.Application.Features.CinemaHalls.Interfaces;

namespace Cinema.Application.Features.CinemaHalls.Facades;

internal class CinemaHallFacade : ICinemaHallFacade
{
    private readonly ICinemaHallService _cinemaHallService;
    private readonly IMapper _mapper;


    public CinemaHallFacade(ICinemaHallService cinemaHallService, IMapper mapper)
    {
        _cinemaHallService = cinemaHallService;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CinemaHallAppResponseDto>> GetAllAsync()
    {
        var cinemaHalls = await _cinemaHallService.GetAllAsync();
        return _mapper.Map<IEnumerable<CinemaHallAppResponseDto>>(cinemaHalls);
    }

    public async Task<CinemaHallAppResponseDto> GetByIdAWithDetailsAsync(int id)
    {
        var person = await _cinemaHallService.GetByIdAsync(id);
        return _mapper.Map<CinemaHallAppResponseDto>(person);
    }

    public async Task DeleteAsync(int id)
    {
        await _cinemaHallService.DeleteAsync(id);
    }
}