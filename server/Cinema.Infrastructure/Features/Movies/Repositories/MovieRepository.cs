using Cinema.Domain.Features.Movies.Entities;
using Cinema.Domain.Features.Movies.Interfaces;
using Cinema.Domain.Shared.Exceptions;
using Cinema.Infrastructure.Core.Data;
using Cinema.Infrastructure.Shared.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Cinema.Infrastructure.Features.Movies.Repositories;

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

    public async Task<List<Movie>> GetByIdsAsync(IEnumerable<int> ids)
    {
        try
        {
            return await _dbContext.Movie.Where(m => ids.Contains(m.Id)).ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while retrieving the movies with ids {ids}.",
                ids);
            throw new DatabaseException($"An error occurred while retrieving the movies with ids {ids}.", e);
        }
    }

    protected override IQueryable<Movie> BuildIncludesQuery(IQueryable<Movie> query)
    {
        return query
            .Include(m => m.MovieActors).ThenInclude(ma => ma.Actor)
            .Include(m => m.DirectedBy);
    }
}