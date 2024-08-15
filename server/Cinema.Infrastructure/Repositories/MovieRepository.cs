using Cinema.Domain.Entities;
using Cinema.Domain.Interfaces;
using Cinema.Infrastructure.Data;
using Microsoft.Extensions.Logging;

namespace Cinema.Infrastructure.Repositories;

internal class MovieRepository : BaseRepository<Movie>, IMovieRepository
{
    public MovieRepository(ApplicationDbContext dbContext, ILogger<MovieRepository> logger
    ) : base(dbContext, logger)
    {
    }
}