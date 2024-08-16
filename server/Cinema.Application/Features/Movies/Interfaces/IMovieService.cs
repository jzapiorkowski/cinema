using Cinema.Domain.Features.Movies.Entities;

namespace Cinema.Application.Features.Movies.Interfaces;

internal interface IMovieService
{
    public Task CreateAsync(Movie movie);
    public Task<Movie> GetByIdAsync(int movieId);
    public Task<IEnumerable<Movie>> GetAllAsync();
    public Task DeleteAsync(int movieId);
    public Task UpdateAsync(Movie movie);
}
