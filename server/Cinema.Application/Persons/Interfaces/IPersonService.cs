using Cinema.Domain.Persons.Entities;

namespace Cinema.Application.Persons.Interfaces;

public interface IPersonService
{
    public Task<List<Person>> GetByIdsAsync(IEnumerable<int> ids);
}