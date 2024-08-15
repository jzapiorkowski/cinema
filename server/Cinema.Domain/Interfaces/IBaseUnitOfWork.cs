namespace Cinema.Domain.Interfaces;

public interface IBaseUnitOfWork : IDisposable
{
    public TRepository Repository<TEntity, TRepository>() where TEntity : class
        where TRepository : class, IBaseRepository<TEntity>;
}