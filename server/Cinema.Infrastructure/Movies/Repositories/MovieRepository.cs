using Cinema.Domain.Movies.Entities;
using Cinema.Domain.Movies.Interfaces;
using Cinema.Infrastructure.Core.Data;
using Cinema.Infrastructure.Shared.Repositories;
using Microsoft.Extensions.Logging;

namespace Cinema.Infrastructure.Movies.Repositories;

internal class MovieRepository : BaseRepository<Movie>, IMovieRepository
{
    public MovieRepository(ApplicationDbContext dbContext, ILogger<MovieRepository> logger
    ) : base(dbContext, logger)
    {
    }
}