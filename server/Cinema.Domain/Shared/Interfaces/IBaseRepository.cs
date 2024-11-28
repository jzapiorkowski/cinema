namespace Cinema.Domain.Shared.Interfaces;

public interface IBaseRepository<TEntity> where TEntity : class
{
    public Task<TEntity> CreateAsync(TEntity entity);
    public Task<TEntity?> GetByIdAsync(int id, bool asNoTracking = false, bool includeAllRelations = false);
    public Task<List<TEntity>> GetAllAsync(bool asNoTracking = false, bool includeAllRelations = false);
    public void Delete(TEntity movie);
    public TEntity Update(TEntity movie);
}