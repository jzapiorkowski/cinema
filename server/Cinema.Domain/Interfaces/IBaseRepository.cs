namespace Cinema.Domain.Interfaces;

public interface IBaseRepository<TEntity> where TEntity : class
{
    public Task Create(TEntity entity);
    public Task<TEntity?> GetByIdAsync(int id);
    public Task<List<TEntity>> GetAllAsync();
    public void Delete(TEntity movie);
    public void Update(TEntity movie);
}