using Cinema.Application.Dto;

namespace Cinema.Application.Interfaces;

public interface IMovieService
{
    public Task CreateMovie(CreateMovieAppDto movie);
    public Task<MovieAppResponseDto?> GetMovieById(int movieId);
    public Task<List<MovieAppResponseDto>> GetAllMovies();
    public Task<bool> DeleteMovie(int movieId);
    public Task<bool> UpdateMovie(int movieId, UpdateMovieAppDto movieDto);
}
