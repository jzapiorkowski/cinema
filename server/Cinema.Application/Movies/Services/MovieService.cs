using AutoMapper;
using Cinema.Application.Movies.Dto;
using Cinema.Application.Movies.Interfaces;
using Cinema.Application.Shared.Exceptions;
using Cinema.Domain.Movies.Entities;
using Cinema.Domain.Movies.Interfaces;
using Cinema.Domain.Shared.Interfaces;
using Microsoft.Extensions.Logging;

namespace Cinema.Application.Movies.Services;

internal class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<MovieService> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public MovieService(IMapper mapper, ILogger<MovieService> logger, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _movieRepository = _unitOfWork.Repository<Movie, IMovieRepository>();
    }

    public async Task CreateMovie(CreateMovieAppDto movie)
    {
        try
        {
            var movieModel = _mapper.Map<Movie>(movie);

            await _movieRepository.Create(movieModel);
            await _unitOfWork.CompleteAsync();
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
            var movie = await _movieRepository.GetByIdAsync(movieId);

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
            throw new AppException("An error occurred while deleting the movie with id {movie.Id}.", e);
        }
    }

    public async Task UpdateMovie(int movieId, UpdateMovieAppDto movieDto)
    {
        try
        {
            var existingMovie = await _movieRepository.GetByIdAsync(movieId);

            if (existingMovie == null)
            {
                throw new NotFoundException("movie", movieId);
            }

            var updatedMovie = _mapper.Map(movieDto, existingMovie);

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
            throw new AppException("An error occurred while updating the movie with id {movieId}.", e);
        }
    }
}