using Cinema.Domain.Features.Persons.Entities;
using Cinema.Domain.Features.Persons.Repositories;
using Cinema.Domain.Shared.Exceptions;
using Cinema.Infrastructure.Core.Data;
using Cinema.Infrastructure.Shared.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Cinema.Infrastructure.Features.Persons.Repositories;

internal class PersonRepository : BaseRepository<Person>, IPersonRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<PersonRepository> _logger;

    public PersonRepository(ApplicationDbContext dbContext, ILogger<PersonRepository> logger) : base(dbContext,
        logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<List<Person>> GetByIdsAsync(IEnumerable<int> ids)
    {
        try
        {
            return await _dbContext.Person.Where(p => ids.Contains(p.Id)).ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while retrieving the actors with ids {ids}.",
                ids);
            throw new DatabaseException($"An error occurred while retrieving the actors with ids {ids}.", e);
        }
    }

    public async Task<Person?> GetWithDetailsByIdAsync(int id, bool asNoTracking)
    {
        try
        {
            IQueryable<Person> query = _dbContext.Person;

            if (asNoTracking)
                query = query.AsNoTracking();

            return await query
                .Include(p => p.MovieActors).ThenInclude(ma => ma.Movie).ThenInclude(m => m.DirectedBy)
                .Include(p => p.DirectedMovies)
                .SingleOrDefaultAsync(p => p.Id == id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}