using Cinema.Application.Persons.Interfaces;
using Cinema.Application.Shared.Exceptions;
using Cinema.Domain.Persons.Entities;

namespace Cinema.Application.Persons.Services;

internal class PersonValidationService : IPersonValidationService
{
    private readonly IPersonService _personService;
    
    public PersonValidationService(IPersonService personService)
    {
        _personService = personService;
    }

    public async Task<List<Person>> ValidatePersonsAsync(IEnumerable<int> personIds, string roleName)
    {
        var foundPersons = await _personService.GetByIdsAsync(personIds);

        if (foundPersons.Count == personIds.ToList().Count) return foundPersons;
        
        var foundPersonsIds = foundPersons.Select(person => person.Id).ToList();
        var missingIds = personIds.Except(foundPersonsIds).ToList();
            
        throw new NotFoundException(roleName, missingIds);

    }
}