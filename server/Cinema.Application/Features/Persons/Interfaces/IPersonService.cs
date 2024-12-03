using Cinema.Domain.Core.Pagination;
using Cinema.Domain.Features.Persons.Entities;

namespace Cinema.Application.Features.Persons.Interfaces;

internal interface IPersonService
{
    public Task<List<Person>> GetByIdsAsync(IEnumerable<int> ids);
    public Task<PaginationResponse<Person>> GetAllAsync(PaginationRequest paginationRequest);
    public Task<Person> GetByIdAsync(int id);
    public Task DeleteAsync(int id);
    public Task<Person> CreateAsync(Person person);
    public Task<Person> UpdateAsync(Person person);
}