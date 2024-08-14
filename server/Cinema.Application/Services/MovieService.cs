using AutoMapper;
using Cinema.Application.Dto;
using Cinema.Application.Exceptions;
using Cinema.Application.Interfaces;
using Cinema.Domain.Entities;
using Cinema.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Cinema.Application.Services;

internal class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<MovieService> _logger;

    public MovieService(IMovieRepository movieRepository, IMapper mapper, ILogger<MovieService> logger)
    {
        _movieRepository = movieRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task CreateMovie(CreateMovieAppDto movie)
    {
        try
        {
            var movieModel = _mapper.Map<Movie>(movie);

            await _movieRepository.CreateMovie(movieModel);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while creating the movie.");
            throw new AppException("An error occurred while creating the movie.", e);
        }
    }

    public async Task<MovieAppResponseDto> GetMovieById(int movieId)
    {
        try
        {
            var movie = await _movieRepository.GetMovieById(movieId);

            if (movie == null)
            {
                throw new NotFoundException("movie", movieId);
            }

            return _mapper.Map<MovieAppResponseDto>(movie);
        }
        catch (NotFoundException)
        {
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while retrieving the movie with id {movieId}.", movieId);
            throw new AppException("An error occurred while retrieving the movie with id {movieId}.", e);
        }
    }

    public async Task<IEnumerable<MovieAppResponseDto>> GetAllMovies()
    {
        try
        {
            var movies = await _movieRepository.GetAllMovies();

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
            var movie = await _movieRepository.GetMovieById(movieId);

            if (movie == null)
            {
                throw new NotFoundException("movie", movieId);
            }

            await _movieRepository.DeleteMovie(movie);
        }
        catch (NotFoundException)
        {
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while deleting the movie with id {movieId}.", movieId);
            throw new AppException("An error occurred while deleting the movie with id {movie.Id}.", e);
        }
    }

    public async Task UpdateMovie(int movieId, UpdateMovieAppDto movieDto)
    {
        try
        {
            var existingMovie = await _movieRepository.GetMovieById(movieId);

            if (existingMovie == null)
            {
                throw new NotFoundException("movie", movieId);
            }

            var updatedMovie = _mapper.Map(movieDto, existingMovie);

            await _movieRepository.UpdateMovie(updatedMovie);
        }
        catch (NotFoundException)
        {
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while updating the movie with id {movieId}.", movieId);
            throw new AppException("An error occurred while updating the movie with id {movieId}.", e);
        }
    }
}