using Cinema.Domain.Features.Movies.Entities;

namespace Cinema.Application.Features.Movies.Interfaces;

internal interface IMovieRelatedEntityValidator
{
    Task<List<Movie>> ValidateEntitiesAsync(List<int> movieIds);
    Task<Movie> ValidateEntityAsync(int movieId);
}