using Cinema.Domain.Persons.Entities;

namespace Cinema.Application.Persons.Interfaces;

internal interface IPersonValidationService
{
    Task<List<Person>> ValidatePersonsAsync(IEnumerable<int> personIds, string roleName);
}