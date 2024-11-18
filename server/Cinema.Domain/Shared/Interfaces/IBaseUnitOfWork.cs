namespace Cinema.Domain.Shared.Interfaces;

public interface IBaseUnitOfWork : IAsyncDisposable
{
    public TRepository Repository<TEntity, TRepository>() where TEntity : class
        where TRepository : class, IBaseRepository<TEntity>;
}