using AutoMapper;
using Cinema.Application.Features.Screenings.Dto;
using Cinema.Application.Features.Screenings.Interfaces;
using Cinema.Domain.Features.Screenings.Entities;

namespace Cinema.Application.Features.Screenings.Facades;

internal class ScreeningFacade : IScreeningFacade
{
    private readonly IMapper _mapper;
    private readonly IScreeningService _screeningService;
    private readonly IScreeningBuilder _screeningBuilder;
    private readonly IScreeningValidator _screeningValidator;

    public ScreeningFacade(IMapper mapper, IScreeningService screeningService, IScreeningBuilder screeningBuilder,
        IScreeningValidator screeningValidator
    )
    {
        _mapper = mapper;
        _screeningService = screeningService;
        _screeningBuilder = screeningBuilder;
        _screeningValidator = screeningValidator;
    }

    public async Task<ScreeningWithDetailsAppResponseDto> GetByIdAsync(int id)
    {
        var screening = await _screeningService.GetByIdAsync(id);
        return _mapper.Map<ScreeningWithDetailsAppResponseDto>(screening);
    }

    public async Task<IEnumerable<ScreeningWithDetailsAppResponseDto>> GetAllWithDetailsAsync(DateTime date)
    {
        var screenings = await _screeningService.GetAllWithDetailsAsync(date);
        return _mapper.Map<IEnumerable<ScreeningWithDetailsAppResponseDto>>(screenings);
    }

    public async Task<ScreeningAppResponseDto> CreateAsync(CreateScreeningAppDto createScreeningDto)
    {
        await _screeningValidator.ValidateAsync(createScreeningDto.MovieId, createScreeningDto.CinemaHallId,
            createScreeningDto.StartTime);

        var screening = BuildScreening(createScreeningDto.StartTime, createScreeningDto.MovieId,
            createScreeningDto.CinemaHallId);

        var createdScreening = await _screeningService.CreateAsync(screening);

        return _mapper.Map<ScreeningAppResponseDto>(createdScreening);
    }

    public async Task<ScreeningAppResponseDto> UpdateAsync(int id, UpdateScreeningAppDto updateScreeningDto)
    {
        await _screeningValidator.ValidateAsync(updateScreeningDto.MovieId, updateScreeningDto.CinemaHallId,
            updateScreeningDto.StartTime);

        var screening = BuildScreening(updateScreeningDto.StartTime, updateScreeningDto.MovieId,
            updateScreeningDto.CinemaHallId, id);

        var updatedScreening = await _screeningService.UpdateAsync(screening);

        return _mapper.Map<ScreeningAppResponseDto>(updatedScreening);
    }

    private Screening BuildScreening(DateTime startTime, int movieId, int cinemaHallId, int? id = null)
    {
        var builder = _screeningBuilder
            .SetStartTime(startTime)
            .SetMovieId(movieId)
            .SetCinemaHallId(cinemaHallId);

        if (id.HasValue)
        {
            builder.SetId(id.Value);
        }

        return builder.Build();
    }
}