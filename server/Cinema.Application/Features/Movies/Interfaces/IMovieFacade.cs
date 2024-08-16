using Cinema.Application.Features.Movies.Dto;

namespace Cinema.Application.Features.Movies.Interfaces;

public interface IMovieFacade
{
    public Task CreateAsync(CreateMovieAppDto movie);
    public Task<MovieWithActorAppResponseDto> GetByIdAsync(int movieId);
    public Task<IEnumerable<MovieAppResponseDto>> GetAllAsync();
    public Task DeleteAsync(int movieId);
    public Task UpdateAsync(int movieId, UpdateMovieAppDto movieDto);
}