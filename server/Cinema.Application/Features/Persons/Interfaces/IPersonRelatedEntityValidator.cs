using Cinema.Domain.Features.Persons.Entities;

namespace Cinema.Application.Features.Persons.Interfaces;

internal interface IPersonRelatedEntityValidator
{
    Task<List<Person>> ValidateEntitiesAsync(List<int> personIds, string roleName);
}