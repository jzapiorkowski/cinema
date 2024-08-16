using Cinema.Domain.Persons.Entities;
using Cinema.Domain.Shared.Interfaces;

namespace Cinema.Domain.Persons.Repositories;

public interface IPersonRepository : IBaseRepository<Person>
{
    public Task<List<Person>> GetByIdsAsync(IEnumerable<int> ids);
}