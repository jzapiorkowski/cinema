using AutoMapper;
using Cinema.Application.Features.Movies.Dto;
using Cinema.Application.Features.Movies.Interfaces;
using Cinema.Application.Features.Persons.Interfaces;
using Cinema.Domain.Core.Pagination;

namespace Cinema.Application.Features.Movies.Facades;

internal class MovieFacade : IMovieFacade
{
    private readonly IPersonRelatedEntityValidator _personRelatedEntityValidator;
    private readonly IMapper _mapper;
    private readonly IMovieService _movieService;
    private readonly IMovieBuilder _movieBuilder;

    public MovieFacade(IPersonRelatedEntityValidator personRelatedEntityValidator, IMapper mapper, IMovieService movieService,
        IMovieBuilder movieBuilder)
    {
        _personRelatedEntityValidator = personRelatedEntityValidator;
        _mapper = mapper;
        _movieService = movieService;
        _movieBuilder = movieBuilder;
    }

    public async Task<MovieAppResponseDto> CreateAsync(CreateMovieAppDto createMovieAppDto)
    {
        await _personRelatedEntityValidator.ValidateEntitiesAsync(
            createMovieAppDto.Actors.Select(actor => actor.Id).ToList(), "actor");
        await _personRelatedEntityValidator.ValidateEntitiesAsync([createMovieAppDto.DirectorId], "director");

        var movie = _movieBuilder
            .SetTitle(createMovieAppDto.Title)
            .SetReleaseDate(createMovieAppDto.ReleaseDate)
            .AddActors(createMovieAppDto.Actors.Select(dto => (dto.Id, dto.Role)))
            .SetGenre(createMovieAppDto.Genre)
            .SetDuration(createMovieAppDto.Duration)
            .SetDirectorId(createMovieAppDto.DirectorId)
            .Build();

        var createdMovie = await _movieService.CreateAsync(movie);

        return _mapper.Map<MovieAppResponseDto>(createdMovie);
    }

    public async Task<MovieWithDetailsAppResponseDto> GetByIdAsync(int movieId)
    {
        var movie = await _movieService.GetByIdAsync(movieId);
        return _mapper.Map<MovieWithDetailsAppResponseDto>(movie);
    }

    public async Task<PaginationResponse<MovieAppResponseDto>> GetAllAsync(PaginationRequest paginationRequest)
    {
        var movies = await _movieService.GetAllAsync(paginationRequest);
        return _mapper.Map<PaginationResponse<MovieAppResponseDto>>(movies);
    }

    public async Task DeleteAsync(int movieId)
    {
        await _movieService.DeleteAsync(movieId);
    }

    public async Task<MovieAppResponseDto> UpdateAsync(int movieId, UpdateMovieAppDto movieDto)
    {
        await _personRelatedEntityValidator.ValidateEntitiesAsync(
            movieDto.Actors.Select(actor => actor.Id).ToList(), "actor");
        await _personRelatedEntityValidator.ValidateEntitiesAsync([movieDto.DirectorId], "director");

        var movie = _movieBuilder
            .SetId(movieId)
            .SetTitle(movieDto.Title)
            .SetReleaseDate(movieDto.ReleaseDate)
            .AddActors(movieDto.Actors.Select(dto => (dto.Id, dto.Role)))
            .SetGenre(movieDto.Genre)
            .SetDirectorId(movieDto.DirectorId)
            .Build();

        var updatedMovie = await _movieService.UpdateAsync(movie);

        return _mapper.Map<MovieAppResponseDto>(updatedMovie);
    }
}