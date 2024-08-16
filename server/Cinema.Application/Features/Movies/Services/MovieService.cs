using AutoMapper;
using Cinema.Application.Features.Movies.Dto;
using Cinema.Application.Features.Movies.Interfaces;
using Cinema.Application.Features.Persons.Interfaces;
using Cinema.Application.Shared.Exceptions;
using Cinema.Domain.Features.MovieActors.Entities;
using Cinema.Domain.Features.Movies.Entities;
using Cinema.Domain.Features.Movies.Interfaces;
using Cinema.Domain.Shared.Interfaces;
using Microsoft.Extensions.Logging;

namespace Cinema.Application.Features.Movies.Services;

internal class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<MovieService> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPersonValidationService _personValidationService;

    public MovieService(IMapper mapper, ILogger<MovieService> logger, IUnitOfWork unitOfWork,
        IPersonValidationService personValidationService)
    {
        _mapper = mapper;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _movieRepository = _unitOfWork.Repository<Movie, IMovieRepository>();
        _personValidationService = personValidationService;
    }

    public async Task CreateMovie(CreateMovieAppDto createMovieAppDto)
    {
        try
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

            await _movieRepository.Create(movie);
            await _unitOfWork.CompleteAsync();
        }
        catch (NotFoundException)
        {
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while creating the movie.");
            throw new AppException("An error occurred while creating the movie.", e);
        }
    }

    public async Task<MovieWithActorAppResponseDto> GetMovieById(int movieId)
    {
        try
        {
            var movie = await _movieRepository.GetWithDetailsByIdAsync(movieId);

            if (movie == null)
            {
                throw new NotFoundException("movie", movieId);
            }

            return _mapper.Map<MovieWithActorAppResponseDto>(movie);
        }
        catch (NotFoundException)
        {
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while retrieving the movie with id {movieId}.", movieId);
            throw new AppException($"An error occurred while retrieving the movie with id {movieId}.", e);
        }
    }

    public async Task<IEnumerable<MovieAppResponseDto>> GetAllMovies()
    {
        try
        {
            var movies = await _movieRepository.GetAllAsync();

            return _mapper.Map<List<MovieAppResponseDto>>(movies);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while retrieving the movies");
            throw new AppException("An error occurred while retrieving the movies", e);
        }
    }

    public async Task DeleteMovie(int movieId)
    {
        try
        {
            var movie = await _movieRepository.GetByIdAsync(movieId);

            if (movie == null)
            {
                throw new NotFoundException("movie", movieId);
            }

            _movieRepository.Delete(movie);
            await _unitOfWork.CompleteAsync();
        }
        catch (NotFoundException)
        {
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while deleting the movie with id {movieId}.", movieId);
            throw new AppException($"An error occurred while deleting the movie with id {movieId}.", e);
        }
    }

    public async Task UpdateMovie(int movieId, UpdateMovieAppDto updateMovieAppDto)
    {
        try
        {
            var existingMovie = await _movieRepository.GetWithDetailsByIdAsync(movieId);

            if (existingMovie == null)
            {
                throw new NotFoundException("movie", movieId);
            }

            var actors =
                await _personValidationService.ValidatePersonsAsync(
                    updateMovieAppDto.Actors.Select(actor => actor.Id).ToList(), "actor");

            var updatedMovie = _mapper.Map(updateMovieAppDto, existingMovie);
            updatedMovie.MovieActors = updateMovieAppDto.Actors
                .Join(actors, dto => dto.Id, actor => actor.Id, (dto, actor) => new MovieActor
                {
                    ActorId = actor.Id,
                    Role = dto.Role
                })
                .ToList();

            _movieRepository.Update(updatedMovie);
            await _unitOfWork.CompleteAsync();
        }
        catch (NotFoundException)
        {
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while updating the movie with id {movieId}.", movieId);
            throw new AppException($"An error occurred while updating the movie with id {movieId}.", e);
        }
    }
}