using Cinema.Domain.Core.Pagination;
using Cinema.Domain.Shared.Exceptions;
using Cinema.Domain.Shared.Interfaces;
using Cinema.Infrastructure.Core.Data;
using Cinema.Infrastructure.Core.Pagination;
using Cinema.Infrastructure.Shared.Exceptions;
using EntityFramework.Exceptions.Common;
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

    protected abstract IQueryable<TEntity> BuildIncludesQuery(IQueryable<TEntity> query);

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        return await ExecuteDbOperation(async () =>
        {
            var createdEntity = await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return createdEntity.Entity;
        }, "creating");
    }

    public async Task DeleteAsync(TEntity entity)
    {
        await ExecuteDbOperation(async () =>
        {
            _dbContext.Set<TEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
            return Task.CompletedTask;
        }, "deleting");
    }

    public async Task<PaginationResponse<TEntity>> GetAllAsync(PaginationRequest paginationRequest,
        bool asNoTracking = true,
        bool includeAllRelations = false)
    {
        return await ExecuteDbOperation(async () =>
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();

            if (asNoTracking)
                query = query.AsNoTracking();

            if (includeAllRelations)
                query = BuildIncludesQuery(query);

            return await Paginator.PaginateAsync(query, paginationRequest);
        }, "retrieving all");
    }

    public async Task<TEntity?> GetByIdAsync(int id, bool asNoTracking = false, bool includeAllRelations = false)
    {
        return await ExecuteDbOperation(async () =>
            {
                IQueryable<TEntity> query = _dbContext.Set<TEntity>();

                if (asNoTracking)
                    query = query.AsNoTracking();

                if (includeAllRelations)
                    query = BuildIncludesQuery(query);

                return await query.FirstOrDefaultAsync(entity => EF.Property<object>(entity, "Id").Equals(id));
            }, $"retrieving by id {id}");
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        return await ExecuteDbOperation(async () =>
        {
            var updatedEntity = _dbContext.Set<TEntity>().Update(entity);
            await _dbContext.SaveChangesAsync();
            return updatedEntity.Entity;
        }, "updating");
    }

    protected async Task<T> ExecuteDbOperation<T>(Func<Task<T>> operation, string operationDescription)
    {
        try
        {
            return await operation();
        }
        catch (UniqueConstraintException e)
        {
            var postgresEx = e.InnerException as PostgresException;
            _logger.LogError(e, "Constraint violation ({constraintName}) while {operation} the {entityName}.",
                postgresEx.ConstraintName, operationDescription, GetEntityName());

            if (postgresEx is { ConstraintName: not null })
            {
                var errorMessage = ConstraintErrorMessages.GetErrorMessage(postgresEx.ConstraintName);
                throw new DuplicateEntityException(errorMessage, e);
            }

            throw new DuplicateEntityException($"A unique constraint violation occurred: {e.Message}", e);
        }
        catch (ReferenceConstraintException e)
        {
            var postgresEx = e.InnerException as PostgresException;
            _logger.LogError(e, "Reference constraint violation while {operation} the {entityName}.",
                operationDescription,
                GetEntityName());

            if (postgresEx is { ConstraintName: not null })
            {
                var errorMessage = ConstraintErrorMessages.GetErrorMessage(postgresEx.ConstraintName);
                throw new EntityReferenceViolationException(errorMessage, e);
            }

            throw new EntityReferenceViolationException($"A reference constraint violation occurred: {e.Message}", e);
        }
        catch (InvalidSortByException e)
        {
            _logger.LogError(e, "Invalid SortBy property while {operation} the {entityName}.",
                operationDescription, GetEntityName());
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while {operation} the {entityName}.",
                operationDescription, GetEntityName());
            throw new DatabaseException($"An error occurred while {operationDescription} the {GetEntityName()}.", e);
        }
    }

    private static string GetEntityName()
    {
        return typeof(TEntity).Name;
    }
}