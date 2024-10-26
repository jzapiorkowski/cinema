using AutoMapper;
using Cinema.API.Core.Validators;
using Cinema.API.Features.Screenings.Dto;
using Cinema.Application.Features.Screenings.Dto;
using Cinema.Application.Features.Screenings.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.API.Features.Screenings.Controllers;

[ApiController]
[Route("screenings")]
[Produces("application/json")]
public class ScreeningController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IScreeningFacade _screeningFacade;

    public ScreeningController(IMapper mapper, IScreeningFacade screeningFacade)
    {
        _mapper = mapper;
        _screeningFacade = screeningFacade;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType<ScreeningWithDetailsApiResponseDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetWithDetailsByIdAsync(int id)
    {
        var screening = await _screeningFacade.GetWithDetailsByIdAsync(id);
        return Ok(_mapper.Map<ScreeningWithDetailsApiResponseDto>(screening));
    }

    [HttpGet("date/{date:datetime}")]
    [ProducesResponseType<IEnumerable<ScreeningWithDetailsApiResponseDto>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllWithDetailsAsync([FromRoute, DateOnly] DateTime date)
    {
        date = DateTime.SpecifyKind(date.Date, DateTimeKind.Utc);

        var screenings = await _screeningFacade.GetAllWithDetailsAsync(date);
        return Ok(_mapper.Map<IEnumerable<ScreeningWithDetailsApiResponseDto>>(screenings));
    }

    [HttpPost]
    [ProducesResponseType<ScreeningApiResponseDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateScreeningApiDto screening)
    {
        var createdScreening = await _screeningFacade.CreateAsync(_mapper.Map<CreateScreeningAppDto>(screening));
        var screeningApiResponse = _mapper.Map<ScreeningApiResponseDto>(createdScreening);

        return CreatedAtAction(nameof(GetWithDetailsByIdAsync), new { id = screeningApiResponse.Id },
            screeningApiResponse);
    }
}