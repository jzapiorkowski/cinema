using Cinema.Application.Features.Movies.Interfaces;
using Cinema.Application.Shared.Validators;
using Cinema.Domain.Features.Movies.Entities;

namespace Cinema.Application.Features.Movies.Validators;

internal class MovieRelatedEntityValidator : RelatedEntityExistenceValidator<Movie, IMovieService>,
    IMovieRelatedEntityValidator
{
    public MovieRelatedEntityValidator(IMovieService movieService) : base(movieService)
    {
    }

    public async Task<List<Movie>> ValidateEntitiesAsync(List<int> movieIds)
    {
        return await base.ValidateEntitiesAsync(movieIds, "movie");
    }

    public async Task<Movie> ValidateEntityAsync(int movieId)
    {
        return await base.ValidateEntityAsync(movieId, "movie");
    }

    protected override async Task<List<Movie>> GetEntitiesByIdsAsync(IEnumerable<int> ids)
    {
        return await _service.GetByIdsAsync(ids.ToList());
    }
    
    protected override async Task<Movie> GetEntityByIdAsync(int id)
    {
        return await _service.GetByIdAsync(id);
    }

    protected override int GetEntityId(Movie entity)
    {
        return entity.Id;
    }
}