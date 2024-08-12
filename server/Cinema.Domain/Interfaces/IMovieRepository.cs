using Cinema.Domain.Entities;

namespace Cinema.Domain.Interfaces;

public interface IMovieRepository
{
    Task CreateMovie(Movie movie);
    Task<Movie?> GetMovieById(int movieId);
    Task<List<Movie>> GetAllMovies();
    Task<bool> DeleteMovie(int movieId);
    Task UpdateMovie(Movie movie);
}