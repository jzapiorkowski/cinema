using Cinema.Domain.Entities;
using Cinema.Domain.Exceptions;
using Cinema.Domain.Interfaces;
using Cinema.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Cinema.Infrastructure.Repositories;

internal class MovieRepository : IMovieRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<MovieRepository> _logger;

    public MovieRepository(ApplicationDbContext dbContext, ILogger<MovieRepository> logger
    )
    {
        _dbContext = dbContext;
        _logger = logger;
    }
    
    public async Task CreateMovie(Movie movie)
    {
        try
        {
            await _dbContext.Movies.AddAsync(movie);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while creating the movie.");
            throw new DatabaseException("An error occurred while creating the movie.", e);
        }
    }

    public async Task<Movie?> GetMovieById(int movieId)
    {
        try
        {
            return await _dbContext.Movies.FindAsync(movieId);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while retrieving the movie with id {movieId}.", movieId);
            throw new DatabaseException("An error occurred while retrieving the movie with id {movieId}.", e);
        }
    }

    public async Task<IEnumerable<Movie>> GetAllMovies()
    {
        try
        {
            return await _dbContext.Movies.ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while retrieving all movies.");
            throw new DatabaseException("An error occurred while retrieving all movies.", e);
        }
    }

    public async Task DeleteMovie(Movie movie)
    {
        try
        {
            _dbContext.Movies.Remove(movie);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while deleting the movie with id {movieId}.", movie.Id);
            throw new DatabaseException("An error occurred while deleting the movie with id {movie.Id}.", e);
        }
    }

    public async Task UpdateMovie(Movie movie)
    {
        try
        {
            _dbContext.Movies.Update(movie);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while updating the movie with id {movieId}.", movie.Id);
            throw new DatabaseException("An error occurred while updating the movie with id {movie.Id}.", e);
        }
    }
}