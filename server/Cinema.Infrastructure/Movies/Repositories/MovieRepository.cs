using Cinema.Domain.Movies.Entities;
using Cinema.Domain.Movies.Interfaces;
using Cinema.Domain.Shared.Exceptions;
using Cinema.Infrastructure.Core.Data;
using Cinema.Infrastructure.Shared.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Cinema.Infrastructure.Movies.Repositories;

internal class MovieRepository : BaseRepository<Movie>, IMovieRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<MovieRepository> _logger;

    public MovieRepository(ApplicationDbContext dbContext, ILogger<MovieRepository> logger
    ) : base(dbContext, logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Movie?> GetWithDetailsByIdAsync(int id)
    {
        try
        {
            return await _dbContext.Movie.Include(m => m.MovieActors).ThenInclude(ma => ma.Actor)
                .SingleOrDefaultAsync(m => m.Id == id);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while retrieving the movie with id {id}.",
                id);
            throw new DatabaseException($"An error occurred while retrieving the movie with id {id}.", e);
        }
    }
}