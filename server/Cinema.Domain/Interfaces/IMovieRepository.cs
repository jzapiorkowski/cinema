using Cinema.Domain.Entities;

namespace Cinema.Domain.Interfaces;

public interface IMovieRepository
{
    public Task CreateMovie(Movie movie);
    public Task<Movie?> GetMovieById(int movieId);
    public Task<IEnumerable<Movie>> GetAllMovies();
    public Task DeleteMovie(Movie movie);
    public Task UpdateMovie(Movie movie);
}