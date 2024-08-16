using Cinema.Domain.Features.Persons.Entities;

namespace Cinema.Application.Features.Persons.Interfaces;

internal interface IPersonValidationService
{
    Task<List<Person>> ValidatePersonsAsync(IEnumerable<int> personIds, string roleName);
}