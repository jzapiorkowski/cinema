using Cinema.Domain.Features.Movies.Entities;
using Cinema.Domain.Features.Movies.Interfaces;
using Cinema.Infrastructure.Core.Data;
using Cinema.Infrastructure.Shared.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Cinema.Infrastructure.Features.Movies.Repositories;

internal class MovieRepository : BaseRepository<Movie>, IMovieRepository
{
    private readonly ApplicationDbContext _dbContext;

    public MovieRepository(ApplicationDbContext dbContext, ILogger<MovieRepository> logger
    ) : base(dbContext, logger)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Movie>> GetByIdsAsync(IEnumerable<int> ids)
    {
        return await ExecuteDbOperation(async () =>
        {
            return await _dbContext.Movie.Where(m => ids.Contains(m.Id)).ToListAsync();
        }, "retrieving by ids");
    }

    protected override IQueryable<Movie> BuildIncludesQuery(IQueryable<Movie> query)
    {
        return query
            .Include(m => m.MovieActors).ThenInclude(ma => ma.Actor)
            .Include(m => m.DirectedBy);
    }
}