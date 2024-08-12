using Cinema.Domain.Entities;
using Cinema.Domain.Interfaces;
using Cinema.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Infrastructure.Repositories;

public class MovieRepository : IMovieRepository
{
    private readonly ApplicationDbContext _dbContext;

    public MovieRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateMovie(Movie movie)
    {
        await _dbContext.Movies.AddAsync(movie);
            await _dbContext.SaveChangesAsync();
    }

    public async Task<Movie?> GetMovieById(int movieId)
    {
        return await _dbContext.FindAsync<Movie>(movieId);
    }

    public async Task<List<Movie>> GetAllMovies()
    {
        return await _dbContext.Movies.ToListAsync();
    }
    
    public async Task<bool> DeleteMovie(int movieId)
    {
        var movie = await _dbContext.Movies.FindAsync(movieId);
    
        if (movie == null)
        {
            return false;
        }
    
        _dbContext.Movies.Remove(movie);
        await _dbContext.SaveChangesAsync();
    
        return true;
    }
    
    public async Task UpdateMovie(Movie movie)
    {
        _dbContext.Movies.Update(movie);
        await _dbContext.SaveChangesAsync();
    }
}
