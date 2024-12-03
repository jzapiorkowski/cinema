using AutoMapper;
using Cinema.API.Features.Persons.Dto;
using Cinema.Application.Features.Persons.Dto;
using Cinema.Application.Features.Persons.Interfaces;
using Cinema.Domain.Core.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.API.Features.Persons.Controllers;

[ApiController]
[Route("persons")]
[Produces("application/json")]
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
    [ProducesResponseType<PaginationResponse<PersonApiResponseDto>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationRequest paginationRequest)
    {
        var persons = await _personFacade.GetAllAsync(paginationRequest);
        return Ok(_mapper.Map<PaginationResponse<PersonApiResponseDto>>(persons));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType<PersonWithDetailsApiResponseDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var person = await _personFacade.GetByIdAsync(id);
        return Ok(_mapper.Map<PersonWithDetailsApiResponseDto>(person));
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _personFacade.DeleteAsync(id);
        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType<PersonApiResponseDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAsync([FromBody] CreatePersonApiDto person)
    {
        var createdPerson = await _personFacade.CreateAsync(_mapper.Map<CreatePersonAppDto>(person));
        var personApiResponse = _mapper.Map<PersonApiResponseDto>(createdPerson);

        return CreatedAtAction(nameof(GetByIdAsync), new { id = personApiResponse.Id },
            personApiResponse);
    }
    
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType<PersonApiResponseDto>(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdatePersonApiDto person)
    {
        var updatedPerson = await _personFacade.UpdateAsync(id, _mapper.Map<UpdatePersonAppDto>(person));
        return Ok(_mapper.Map<PersonApiResponseDto>(updatedPerson));
    }
}