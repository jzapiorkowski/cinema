using AutoMapper;
using Cinema.Application.Features.CinemaHalls.Interfaces;
using Cinema.Application.Features.Movies.Interfaces;
using Cinema.Application.Features.Screenings.Dto;
using Cinema.Application.Features.Screenings.Interfaces;

namespace Cinema.Application.Features.Screenings.Facades;

internal class ScreeningFacade : IScreeningFacade
{
    private readonly IMapper _mapper;
    private readonly IScreeningService _screeningService;
    private readonly IScreeningBuilder _screeningBuilder;
    private readonly IMovieRelatedEntityValidator _movieRelatedEntityValidator;
    private readonly ICinemaHallRelatedEntityValidator _cinemaHallRelatedEntityValidator;


    public ScreeningFacade(IMapper mapper, IScreeningService screeningService, IScreeningBuilder screeningBuilder,
        IMovieRelatedEntityValidator movieRelatedEntityValidator,
        ICinemaHallRelatedEntityValidator cinemaHallRelatedEntityValidator
    )
    {
        _mapper = mapper;
        _screeningService = screeningService;
        _screeningBuilder = screeningBuilder;
        _movieRelatedEntityValidator = movieRelatedEntityValidator;
        _cinemaHallRelatedEntityValidator = cinemaHallRelatedEntityValidator;
    }

    public async Task<ScreeningWithDetailsAppResponseDto> GetWithDetailsByIdAsync(int id)
    {
        var screening = await _screeningService.GetWithDetailsByIdAsync(id);
        return _mapper.Map<ScreeningWithDetailsAppResponseDto>(screening);
    }

    public async Task<IEnumerable<ScreeningWithDetailsAppResponseDto>> GetAllWithDetailsAsync(DateTime date)
    {
        var screenings = await _screeningService.GetAllWithDetailsAsync(date);
        return _mapper.Map<IEnumerable<ScreeningWithDetailsAppResponseDto>>(screenings);
    }

    public async Task<ScreeningAppResponseDto> CreateAsync(CreateScreeningAppDto createScreeningDto)
    {
        await _movieRelatedEntityValidator.ValidateEntityAsync(createScreeningDto.MovieId);
        await _cinemaHallRelatedEntityValidator.ValidateEntityAsync(createScreeningDto.CinemaHallId);

        var screening = _screeningBuilder
            .SetStartTime(createScreeningDto.StartTime)
            .SetMovieId(createScreeningDto.MovieId)
            .SetCinemaHallId(createScreeningDto.CinemaHallId)
            .Build();

        var createdScreening = await _screeningService.CreateAsync(screening);

        return _mapper.Map<ScreeningAppResponseDto>(createdScreening);
    }

    public async Task<ScreeningAppResponseDto> UpdateAsync(int id, UpdateScreeningAppDto updateScreeningDto)
    {
        await _movieRelatedEntityValidator.ValidateEntityAsync(updateScreeningDto.MovieId);
        await _cinemaHallRelatedEntityValidator.ValidateEntityAsync(updateScreeningDto.CinemaHallId);

        var screening = _screeningBuilder
            .SetId(id)
            .SetStartTime(updateScreeningDto.StartTime)
            .SetMovieId(updateScreeningDto.MovieId)
            .SetCinemaHallId(updateScreeningDto.CinemaHallId)
            .Build();

        var updatedScreening = await _screeningService.UpdateAsync(screening);

        return _mapper.Map<ScreeningAppResponseDto>(updatedScreening);
    }
}