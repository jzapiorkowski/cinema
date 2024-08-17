using AutoMapper;
using Cinema.Application.Features.Movies.Dto;
using Cinema.Application.Features.Movies.Interfaces;
using Cinema.Application.Features.Persons.Interfaces;

namespace Cinema.Application.Features.Movies.Facades;

internal class MovieFacade : IMovieFacade
{
    private readonly IPersonValidationService _personValidationService;
    private readonly IMapper _mapper;
    private readonly IMovieService _movieService;
    private readonly IMovieBuilder _movieBuilder;

    public MovieFacade(IPersonValidationService personValidationService, IMapper mapper, IMovieService movieService,
        IMovieBuilder movieBuilder)
    {
        _personValidationService = personValidationService;
        _mapper = mapper;
        _movieService = movieService;
        _movieBuilder = movieBuilder;
    }

    public async Task<MovieAppResponseDto> CreateAsync(CreateMovieAppDto createMovieAppDto)
    {
        await _personValidationService.ValidatePersonsAsync(
            createMovieAppDto.Actors.Select(actor => actor.Id).ToList(), "actor");

        var movie = _movieBuilder
            .SetTitle(createMovieAppDto.Title)
            .SetReleaseDate(createMovieAppDto.ReleaseDate)
            .AddActors(createMovieAppDto.Actors.Select(dto => (dto.Id, dto.Role)))
            .SetGenre(createMovieAppDto.Genre)
            .SetDirector(createMovieAppDto.Director)
            .Build();

        var createdMovie = await _movieService.CreateAsync(movie);

        return _mapper.Map<MovieAppResponseDto>(createdMovie);
    }

    public async Task<MovieWithActorAppResponseDto> GetByIdAWithDetailsAsync(int movieId)
    {
        var movie = await _movieService.GetByIdAWithDetailsAsync(movieId);
        return _mapper.Map<MovieWithActorAppResponseDto>(movie);
    }

    public async Task<IEnumerable<MovieAppResponseDto>> GetAllAsync()
    {
        var movies = await _movieService.GetAllAsync();
        return _mapper.Map<IEnumerable<MovieAppResponseDto>>(movies);
    }

    public async Task DeleteAsync(int movieId)
    {
        await _movieService.DeleteAsync(movieId);
    }

    public async Task<MovieAppResponseDto> UpdateAsync(int movieId, UpdateMovieAppDto movieDto)
    {
        var existingMovie = await _movieService.GetByIdAWithDetailsAsync(movieId);

        await _personValidationService.ValidatePersonsAsync(
            movieDto.Actors.Select(actor => actor.Id).ToList(), "actor");

        var movie = _movieBuilder
            .SetId(existingMovie.Id)
            .SetTitle(movieDto.Title)
            .SetReleaseDate(movieDto.ReleaseDate)
            .AddActors(movieDto.Actors.Select(dto => (dto.Id, dto.Role)))
            .SetGenre(movieDto.Genre)
            .SetDirector(movieDto.Director)
            .Build();

        var updatedMovie = await _movieService.UpdateAsync(movie);

        return _mapper.Map<MovieAppResponseDto>(updatedMovie);
    }
}