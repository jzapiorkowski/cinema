using AutoMapper;
using Cinema.API.Features.CinemaHalls.Dto;
using Cinema.Application.Features.CinemaHalls.Dto;
using Cinema.Application.Features.CinemaHalls.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.API.Features.CinemaHalls.Controllers;

[ApiController]
[Route("cinema-halls")]
[Produces("application/json")]
public class CinemaHallController : ControllerBase
{
    private readonly ICinemaHallFacade _cinemaHallFacade;
    private readonly IMapper _mapper;

    public CinemaHallController(ICinemaHallFacade cinemaHallFacade, IMapper mapper)
    {
        _cinemaHallFacade = cinemaHallFacade;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType<List<CinemaHallApiResponseDto>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync()
    {
        var cinemaHalls = await _cinemaHallFacade.GetAllAsync();
        return Ok(_mapper.Map<List<CinemaHallApiResponseDto>>(cinemaHalls));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType<CinemaHallWithDetailsApiResponseDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var cinemaHall = await _cinemaHallFacade.GetByIdWithDetailsAsync(id);
        return Ok(_mapper.Map<CinemaHallWithDetailsApiResponseDto>(cinemaHall));
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(int cinemaHallId)
    {
        await _cinemaHallFacade.DeleteAsync(cinemaHallId);
        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType<CinemaHallApiResponseDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateCinemaHallApiDto createCinemaHallApiDto)
    {
        var createCinemaHallAppDto = _mapper.Map<CreateCinemaHallAppDto>(createCinemaHallApiDto);
        var createdCinemaHall = await _cinemaHallFacade.CreateAsync(createCinemaHallAppDto);
        return CreatedAtAction(nameof(GetByIdAsync), new { cinemaHallId = createdCinemaHall.Id },
            _mapper.Map<CinemaHallApiResponseDto>(createdCinemaHall));
    }
    
    [HttpPut("{id:int}")]
    [ProducesResponseType<CinemaHallApiResponseDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateCinemaHallApiDto updateCinemaHallApiDto)
    {
        var updateCinemaHallAppDto = _mapper.Map<UpdateCinemaHallAppDto>(updateCinemaHallApiDto);
        var updatedCinemaHall = await _cinemaHallFacade.UpdateAsync(id, updateCinemaHallAppDto);
        return Ok(_mapper.Map<CinemaHallApiResponseDto>(updatedCinemaHall));
    }
}