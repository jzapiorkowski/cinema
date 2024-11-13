using Cinema.Domain.Shared.Exceptions;
using Cinema.Domain.Shared.Interfaces;
using Cinema.Infrastructure.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Cinema.Infrastructure.Shared.Repositories;

internal abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<BaseRepository<TEntity>> _logger;

    protected BaseRepository(ApplicationDbContext dbContext, ILogger<BaseRepository<TEntity>> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        try
        {
            var createdEntity = await _dbContext.Set<TEntity>().AddAsync(entity);
            return createdEntity.Entity;
        }
        catch (DbUpdateException e) when (e.InnerException is PostgresException)
        {
            throw new DuplicateEntityException(e.Message, e.InnerException);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while creating the {entityName}.", GetEntityName());
            throw new DatabaseException($"An error occurred while creating the {GetEntityName()}.", e);
        }
    }

    public void Delete(TEntity entity)
    {
        try
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while deleting the {entityName}.", GetEntityName());
            throw new DatabaseException($"An error occurred while deleting the {GetEntityName()}.", e);
        }
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        try
        {
            return await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while retrieving all {entityName}.", GetEntityName());
            throw new DatabaseException($"An error occurred while retrieving all {GetEntityName()}.", e);
        }
    }

    public async Task<TEntity?> GetByIdAsync(int id)
    {
        try
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while retrieving the {entityName} with id {id}.", GetEntityName(),
                id);
            throw new DatabaseException($"An error occurred while retrieving the {GetEntityName()} with id {id}.", e);
        }
    }

    public TEntity Update(TEntity entity)
    {
        try
        {
            var updatedEntity = _dbContext.Set<TEntity>().Update(entity);
            return updatedEntity.Entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while updating the {entityName}.", GetEntityName());
            throw new DatabaseException("An error occurred while updating the movie with id {movie.Id}.", e);
        }
    }

    private static string GetEntityName()
    {
        return typeof(TEntity).Name;
    }
}