using Cinema.Application.Features.Movies.Dto;

namespace Cinema.Application.Features.Movies.Interfaces;

public interface IMovieFacade
{
    public Task<MovieAppResponseDto> CreateAsync(CreateMovieAppDto movie);
    public Task<MovieWithDetailsAppResponseDto> GetByIdAsync(int movieId);
    public Task<IEnumerable<MovieAppResponseDto>> GetAllAsync();
    public Task DeleteAsync(int movieId);
    public Task<MovieAppResponseDto> UpdateAsync(int movieId, UpdateMovieAppDto movieDto);
}