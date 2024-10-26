using Cinema.Application.Shared.Exceptions;

namespace Cinema.Application.Shared.Validators;

internal abstract class RelatedEntityExistenceValidator<TEntity, TService> where TEntity : class where TService : class
{
    protected readonly TService _service;

    protected RelatedEntityExistenceValidator(TService service)
    {
        _service = service;
    }

    protected async Task<List<TEntity>> ValidateEntitiesAsync(List<int> ids, string entityName)
    {
        var foundEntities = await GetEntitiesByIdsAsync(ids);

        if (foundEntities.Count == ids.Count) return foundEntities;

        var foundEntitiesIds = foundEntities.Select(GetEntityId);
        var missingIds = ids.Except(foundEntitiesIds).ToList();

        throw new NotFoundException(entityName, missingIds);
    }

    protected async Task<TEntity> ValidateEntityAsync(int id, string entityName)
    {
        var entity = await GetEntityByIdAsync(id);

        if (entity != null) return entity;

        throw new NotFoundException(entityName, id);
    }

    protected virtual Task<List<TEntity>> GetEntitiesByIdsAsync(IEnumerable<int> ids)
    {
        throw new NotImplementedException($"{nameof(GetEntitiesByIdsAsync)} is not implemented in {GetType().Name}.");
    }

    protected virtual Task<TEntity> GetEntityByIdAsync(int id)
    {
        throw new NotImplementedException($"{nameof(GetEntityByIdAsync)} is not implemented in {GetType().Name}.");
    }

    protected abstract int GetEntityId(TEntity entity);
}