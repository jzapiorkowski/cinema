using Cinema.Application.Features.Movies.Interfaces;
using Cinema.Application.Shared.Exceptions;
using Cinema.Domain.Features.Movies.Entities;
using Cinema.Domain.Features.Movies.Interfaces;
using Cinema.Domain.Shared.Interfaces;
using Microsoft.Extensions.Logging;

namespace Cinema.Application.Features.Movies.Services;

internal class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;
    private readonly ILogger<MovieService> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public MovieService(ILogger<MovieService> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _movieRepository = _unitOfWork.Repository<Movie, IMovieRepository>();
    }

    public async Task<Movie> CreateAsync(Movie movie)
    {
        try
        {
            var createdMovie = await _movieRepository.CreateAsync(movie);
            await _unitOfWork.CompleteAsync();
            return createdMovie;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while creating the movie.");
            throw new AppException("An error occurred while creating the movie.", e);
        }
    }

    public async Task<IEnumerable<Movie>> GetAllAsync()
    {
        try
        {
            return await _movieRepository.GetAllAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while retrieving the movies");
            throw new AppException("An error occurred while retrieving the movies", e);
        }
    }

    public async Task DeleteAsync(int movieId)
    {
        try
        {
            var movie = await GetByIdAsync(movieId);

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

    public async Task<Movie> UpdateAsync(Movie movie)
    {
        try
        {
            await GetByIdAsync(movie.Id, true, false);

            var updatedMovie = _movieRepository.Update(movie);
            await _unitOfWork.CompleteAsync();

            return updatedMovie;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while updating the movie with id {movieId}.", movie.Id);
            throw new AppException($"An error occurred while updating the movie with id {movie.Id}.", e);
        }
    }

    public async Task<List<Movie>> GetByIdsAsync(List<int> ids)
    {
        try
        {
            return await _movieRepository.GetByIdsAsync(ids);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while retrieving people with ids {ids}.", ids);
            throw new AppException($"An error occurred while retrieving people with ids {ids}.", e);
        }
    }

    public async Task<Movie?> GetByIdAsync(int id)
    {
        return await GetByIdAsync(id, true, true);
    }

    private async Task<Movie?> GetByIdAsync(int id, bool asNoTracking, bool includeAllRelations)
    {
        try
        {
            var movie = await
                _movieRepository.GetByIdAsync(id, asNoTracking, includeAllRelations);

            if (movie == null)
            {
                throw new NotFoundException("movie", id);
            }

            return movie;
        }
        catch (NotFoundException)
        {
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while retrieving the movie with id {id}.", id);
            throw new AppException($"An error occurred while retrieving the movie with id {id}.", e);
        }
    }
}