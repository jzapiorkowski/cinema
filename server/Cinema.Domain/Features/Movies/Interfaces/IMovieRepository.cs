using Cinema.Domain.Features.Movies.Entities;
using Cinema.Domain.Shared.Interfaces;

namespace Cinema.Domain.Features.Movies.Interfaces;

public interface IMovieRepository : IBaseRepository<Movie>
{
    public Task<Movie?> GetWithDetailsByIdAsync(int id);
    public Task<List<Movie>> GetByIdsAsync(IEnumerable<int> ids);
}