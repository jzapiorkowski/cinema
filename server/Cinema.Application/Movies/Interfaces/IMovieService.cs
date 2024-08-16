using Cinema.Application.Movies.Dto;

namespace Cinema.Application.Movies.Interfaces;

public interface IMovieService
{
    public Task CreateMovie(CreateMovieAppDto movie);
    public Task<MovieWithActorAppResponseDto> GetMovieById(int movieId);
    public Task<IEnumerable<MovieAppResponseDto>> GetAllMovies();
    public Task DeleteMovie(int movieId);
    public Task UpdateMovie(int movieId, UpdateMovieAppDto movieDto);
}
