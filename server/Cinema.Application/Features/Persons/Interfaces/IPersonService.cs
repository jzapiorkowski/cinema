using Cinema.Domain.Features.Persons.Entities;

namespace Cinema.Application.Features.Persons.Interfaces;

public interface IPersonService
{
    public Task<List<Person>> GetByIdsAsync(IEnumerable<int> ids);
}