using Cinema.Application.Dto;

namespace Cinema.Application.Interfaces;

public interface IMovieService
{
    public Task CreateMovie(CreateMovieAppDto movie);
    public Task<MovieAppResponseDto> GetMovieById(int movieId);
    public Task<IEnumerable<MovieAppResponseDto>> GetAllMovies();
    public Task DeleteMovie(int movieId);
    public Task UpdateMovie(int movieId, UpdateMovieAppDto movieDto);
}
