using AutoMapper;
using Cinema.API.Features.CinemaHalls.Dto;
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
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var screening = await _screeningFacade.GetByIdAsync(id);
        return Ok(_mapper.Map<ScreeningWithDetailsApiResponseDto>(screening));
    }

    [HttpGet("date/{date:datetime}")]
    [ProducesResponseType<IEnumerable<ScreeningWithDetailsApiResponseDto>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllWithDetailsAsync([FromRoute] DateTime date)
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

        return CreatedAtAction(nameof(GetByIdAsync), new { id = screeningApiResponse.Id },
            screeningApiResponse);
    }
    
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ScreeningApiResponseDto>(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateScreeningApiDto screening)
    {
        var updatedScreening = await _screeningFacade.UpdateAsync(id, _mapper.Map<UpdateScreeningAppDto>(screening));
        return Ok(_mapper.Map<ScreeningApiResponseDto>(updatedScreening));
    }
    
    [HttpGet("{screeningId:int}/seats")]
    [ProducesResponseType<List<SeatApiResponseDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAvailableSeatsAsync(int screeningId)
    {
        var availableSeats = await _screeningFacade.GetAvailableSeatsAsync(screeningId);
        return Ok(_mapper.Map<List<SeatApiResponseDto>>(availableSeats));
    }
}