using Cinema.Domain.Features.Persons.Entities;

namespace Cinema.Application.Features.Persons.Interfaces;

internal interface IPersonService
{
    public Task<List<Person>> GetByIdsAsync(IEnumerable<int> ids);
}