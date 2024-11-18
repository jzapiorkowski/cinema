using Cinema.Domain.Features.Movies.Entities;

namespace Cinema.Application.Features.Movies.Interfaces;

internal interface IMovieService
{
    public Task<Movie> CreateAsync(Movie movie);
    public Task<IEnumerable<Movie>> GetAllAsync();
    public Task DeleteAsync(int movieId);
    public Task<Movie> UpdateAsync(Movie movie);
    public Task<List<Movie>> GetByIdsAsync(List<int> moviesIds);
    public Task<Movie?> GetByIdAsync(int movieId);
}