using Cinema.Domain.Features.Persons.Entities;
using Cinema.Domain.Shared.Interfaces;

namespace Cinema.Domain.Features.Persons.Repositories;

public interface IPersonRepository : IBaseRepository<Person>
{
    public Task<List<Person>> GetByIdsAsync(IEnumerable<int> ids);
    public Task<Person?> GetWithDetailsByIdAsync(int id);
}