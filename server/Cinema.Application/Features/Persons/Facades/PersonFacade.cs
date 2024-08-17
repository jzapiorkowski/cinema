using AutoMapper;
using Cinema.Application.Features.Persons.Dto;
using Cinema.Application.Features.Persons.Interfaces;

namespace Cinema.Application.Features.Persons.Facades;

internal class PersonFacade : IPersonFacade
{
    private readonly IPersonService _personService;
    private readonly IMapper _mapper;

    public PersonFacade(IPersonService personService, IMapper mapper)
    {
        _personService = personService;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PersonAppResponseDto>> GetAllAsync()
    {
        var persons = await _personService.GetAllAsync();
        return _mapper.Map<IEnumerable<PersonAppResponseDto>>(persons);
    }

    public async Task<PersonWithDetailsAppResponseDto> GetWithDetailsByIdAsync(int id)
    {
        var person = await _personService.GetWithDetailsByIdAsync(id);
        return _mapper.Map<PersonWithDetailsAppResponseDto>(person);
    }
    
    public async Task DeleteAsync(int id)
    {
        await _personService.DeleteAsync(id);
    }
}