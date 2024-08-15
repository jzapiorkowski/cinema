namespace Cinema.Domain.Interfaces;

public interface IUnitOfWork : IBaseUnitOfWork
{
    public Task<int> CompleteAsync();
}