using Cinema.Domain.Features.Persons.Entities;

namespace Cinema.Application.Features.Persons.Interfaces;

internal interface IPersonService
{
    public Task<List<Person>> GetByIdsAsync(IEnumerable<int> ids);
    public Task<IEnumerable<Person>> GetAllAsync();
    public Task<Person> GetByIdAsync(int id);
    public Task DeleteAsync(int id);
    public Task<Person> CreateAsync(Person person);
    public Task<Person> UpdateAsync(Person person);
}