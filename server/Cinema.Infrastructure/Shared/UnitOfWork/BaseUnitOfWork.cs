using System.Collections.Concurrent;
using Cinema.Domain.Shared.Interfaces;
using Cinema.Infrastructure.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace Cinema.Infrastructure.Shared.UnitOfWork;

internal abstract class BaseUnitOfWork : IBaseUnitOfWork
{
    private readonly DbContext _context;
    private readonly IServiceProvider _serviceProvider;
    private IDbContextTransaction? _transaction;
    private readonly ConcurrentDictionary<Type, object> _repositories;

    protected BaseUnitOfWork(ApplicationDbContext context, IServiceProvider serviceProvider)
    {
        _context = context;
        _serviceProvider = serviceProvider;
        _repositories = new ConcurrentDictionary<Type, object>();
    }

    public TRepository Repository<TEntity, TRepository>() where TEntity : class
        where TRepository : class, IBaseRepository<TEntity>
    {
        var type = typeof(TEntity);

        if (_repositories.ContainsKey(type))
        {
            return _repositories[type] as TRepository;
        }

        var repository = _serviceProvider.GetRequiredService<TRepository>();
        _repositories.TryAdd(type, repository);
        return repository;
    }

    public async void Dispose()
    {
        await _context.DisposeAsync();

        if (_transaction == null) return;
        
        await _transaction.DisposeAsync();
        _transaction = null;
    }
}