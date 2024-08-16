using AutoMapper;
using Cinema.Application.Features.Movies.Dto;
using Cinema.Application.Features.Movies.Interfaces;
using Cinema.Application.Features.Persons.Interfaces;
using Cinema.Domain.Features.MovieActors.Entities;
using Cinema.Domain.Features.Movies.Entities;

namespace Cinema.Application.Features.Movies.Facades;

internal class MovieFacade : IMovieFacade
{
    private readonly IPersonValidationService _personValidationService;
    private readonly IMapper _mapper;
    private readonly IMovieService _movieService;

    public MovieFacade(IPersonValidationService personValidationService, IMapper mapper, IMovieService movieService)
    {
        _personValidationService = personValidationService;
        _mapper = mapper;
        _movieService = movieService;
    }

    public async Task CreateAsync(CreateMovieAppDto createMovieAppDto)
    {
        var actors =
            await _personValidationService.ValidatePersonsAsync(
                createMovieAppDto.Actors.Select(actor => actor.Id).ToList(), "actor");

        var movie = _mapper.Map<Movie>(createMovieAppDto);
        movie.MovieActors = createMovieAppDto.Actors
            .Join(actors, dto => dto.Id, actor => actor.Id, (dto, actor) => new MovieActor
            {
                ActorId = actor.Id,
                Role = dto.Role
            })
            .ToList();

        await _movieService.CreateAsync(movie);
    }

    public async Task<MovieWithActorAppResponseDto> GetByIdAsync(int movieId)
    {
        var movie = await _movieService.GetByIdAsync(movieId);
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

    public async Task UpdateAsync(int movieId, UpdateMovieAppDto movieDto)
    {
        var movie = await _movieService.GetByIdAsync(movieId);

        var actors = await _personValidationService.ValidatePersonsAsync(
            movieDto.Actors.Select(actor => actor.Id).ToList(), "actor");

        _mapper.Map(movieDto, movie);
        movie.MovieActors = movieDto.Actors
            .Join(actors, dto => dto.Id, actor => actor.Id, (dto, actor) => new MovieActor
            {
                ActorId = actor.Id,
                Role = dto.Role
            }).ToList();

        await _movieService.UpdateAsync(movie);
    }
}