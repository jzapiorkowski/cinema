using AutoMapper;
using Cinema.API.Features.Persons.Dto;
using Cinema.Application.Features.Persons.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.API.Features.Persons.Controllers;

[ApiController]
[Route("persons")]
public class PersonController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IPersonFacade _personFacade;

    public PersonController(IMapper mapper, IPersonFacade personFacade)
    {
        _mapper = mapper;
        _personFacade = personFacade;
    }

    [HttpGet]
    [ProducesResponseType<IEnumerable<PersonApiResponseDto>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync()
    {
        var persons = await _personFacade.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<PersonApiResponseDto>>(persons));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType<PersonWithDetailsApiResponseDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdWithDetailsAsync(int id)
    {
        var movie = await _personFacade.GetWithDetailsByIdAsync(id);
        return Ok(_mapper.Map<PersonWithDetailsApiResponseDto>(movie));
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _personFacade.DeleteAsync(id);
        return NoContent();
    }
}