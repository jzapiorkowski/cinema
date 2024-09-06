using Cinema.Application.Features.Persons.Interfaces;
using Cinema.Application.Shared.Validators;
using Cinema.Domain.Features.Persons.Entities;

namespace Cinema.Application.Features.Persons.Validators;

internal class PersonRelatedEntityValidator : RelatedEntityExistenceValidator<Person, IPersonService>,
    IPersonRelatedEntityValidator
{
    public PersonRelatedEntityValidator(IPersonService personService) : base(personService)
    {
    }

    public async Task<List<Person>> ValidateEntitiesAsync(List<int> personIds, string roleName)
    {
        return await base.ValidateEntitiesAsync(personIds, roleName);
    }

    protected override async Task<List<Person>> GetEntitiesByIdsAsync(IEnumerable<int> ids)
    {
        return await _service.GetByIdsAsync(ids);
    }

    protected override int GetEntityId(Person entity)
    {
        return entity.Id;
    }
}