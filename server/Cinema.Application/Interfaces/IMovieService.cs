using Cinema.Application.Dto;

namespace Cinema.Application.Interfaces;

public interface IMovieService
{
    Task CreateMovie(CreateMovieAppDto movie);
    Task<MovieAppResponseDto?> GetMovieById(int movieId);
    Task<List<MovieAppResponseDto>> GetAllMovies();
    Task<bool> DeleteMovie(int movieId);
    Task<bool> UpdateMovie(int movieId, UpdateMovieAppDto movieDto);
}
