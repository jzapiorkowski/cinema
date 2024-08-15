namespace Cinema.Domain.Interfaces;

public interface ITransactionalUnitOfWork : IBaseUnitOfWork
{
    public Task BeginTransactionAsync();
    public Task CommitTransactionAsync();
    public Task RollbackTransactionAsync();
}