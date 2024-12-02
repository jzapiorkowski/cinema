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

    public PersonRepository(ApplicationDbContext dbContext, ILogger<PersonRepository> logger) : base(dbContext,
        logger)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Person>> GetByIdsAsync(IEnumerable<int> ids)
    {
        return await ExecuteDbOperation(
            async () => { return await _dbContext.Person.Where(p => ids.Contains(p.Id)).ToListAsync(); },
            "retrieving by ids.");
    }

    protected override IQueryable<Person> BuildIncludesQuery(IQueryable<Person> query)
    {
        return query
            .Include(p => p.MovieActors).ThenInclude(ma => ma.Movie).ThenInclude(m => m.DirectedBy)
            .Include(p => p.DirectedMovies);
    }
}