namespace Cinema.Domain.Shared.Interfaces;

public interface IUnitOfWork : IBaseUnitOfWork
{
    public Task<int> CompleteAsync();
}