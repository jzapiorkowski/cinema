using Cinema.Domain.Exceptions;
using Cinema.Domain.Interfaces;
using Cinema.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Cinema.Infrastructure;

internal class TransactionalUnitOfWork : BaseUnitOfWork, ITransactionalUnitOfWork
{
    private readonly DbContext _context;

    private IDbContextTransaction? _transaction;

    public TransactionalUnitOfWork(ApplicationDbContext context, IServiceProvider serviceProvider) : base(context,
        serviceProvider)
    {
        _context = context;
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        if (_transaction == null) return;

        try
        {
            await _transaction.CommitAsync();
        }
        catch (Exception e)
        {
            await _transaction.RollbackAsync();
            throw new DatabaseException("An error occurred while committing db transaction", e);
        }
        finally
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async void Dispose()
    {
        await _context.DisposeAsync();

        if (_transaction != null)
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }
}