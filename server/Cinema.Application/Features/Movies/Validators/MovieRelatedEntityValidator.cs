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

    protected override async Task<List<Movie>> GetEntitiesByIdsAsync(IEnumerable<int> ids)
    {
        return await _service.GetByIdsAsync(ids.ToList());
    }

    protected override int GetEntityId(Movie entity)
    {
        return entity.Id;
    }
}