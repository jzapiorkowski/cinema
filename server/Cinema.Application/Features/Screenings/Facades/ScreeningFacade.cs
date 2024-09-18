using AutoMapper;
using Cinema.Application.Features.Screenings.Dto;
using Cinema.Application.Features.Screenings.Interfaces;

namespace Cinema.Application.Features.Screenings.Facades;

internal class ScreeningFacade : IScreeningFacade
{
    private readonly IMapper _mapper;
    private readonly IScreeningService _screeningService;

    public ScreeningFacade(IMapper mapper, IScreeningService screeningService
    )
    {
        _mapper = mapper;
        _screeningService = screeningService;
    }

    public async Task<ScreeningWithDetailsAppResponseDto> GetWithDetailsByIdAsync(int id)
    {
        var person = await _screeningService.GetWithDetailsByIdAsync(id);
        return _mapper.Map<ScreeningWithDetailsAppResponseDto>(person);
    }
}