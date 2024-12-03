using Cinema.Application.Features.Movies.Dto;
using Cinema.Domain.Core.Pagination;

namespace Cinema.Application.Features.Movies.Interfaces;

public interface IMovieFacade
{
    public Task<MovieAppResponseDto> CreateAsync(CreateMovieAppDto movie);
    public Task<MovieWithDetailsAppResponseDto> GetByIdAsync(int movieId);
    public Task<PaginationResponse<MovieAppResponseDto>> GetAllAsync(PaginationRequest paginationRequest);
    public Task DeleteAsync(int movieId);
    public Task<MovieAppResponseDto> UpdateAsync(int movieId, UpdateMovieAppDto movieDto);
}