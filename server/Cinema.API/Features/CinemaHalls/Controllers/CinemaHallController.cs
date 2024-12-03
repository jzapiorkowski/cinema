using AutoMapper;
using Cinema.API.Features.CinemaHalls.Dto;
using Cinema.Application.Features.CinemaHalls.Dto;
using Cinema.Application.Features.CinemaHalls.Interfaces;
using Cinema.Domain.Core.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.API.Features.CinemaHalls.Controllers;

[ApiController]
[Route("cinema-halls")]
[Produces("application/json")]
public class CinemaHallController : ControllerBase
{
    private readonly ICinemaHallFacade _cinemaHallFacade;
    private readonly IMapper _mapper;
    private readonly ICinemaHallSeatFacade _cinemaHallSeatFacade;

    public CinemaHallController(ICinemaHallFacade cinemaHallFacade, IMapper mapper, ICinemaHallSeatFacade cinemaHallSeatFacade)
    {
        _cinemaHallFacade = cinemaHallFacade;
        _mapper = mapper;
        _cinemaHallSeatFacade = cinemaHallSeatFacade;
    }

    [HttpGet]
    [ProducesResponseType<PaginationResponse<CinemaHallApiResponseDto>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationRequest paginationRequest)
    {
        var cinemaHalls = await _cinemaHallFacade.GetAllAsync(paginationRequest);
        return Ok(_mapper.Map<PaginationResponse<CinemaHallApiResponseDto>>(cinemaHalls));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType<CinemaHallWithDetailsApiResponseDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var cinemaHall = await _cinemaHallFacade.GetByIdAsync(id);
        return Ok(_mapper.Map<CinemaHallWithDetailsApiResponseDto>(cinemaHall));
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _cinemaHallFacade.DeleteAsync(id);
        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType<CinemaHallApiResponseDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateCinemaHallApiDto createCinemaHallApiDto)
    {
        var createCinemaHallAppDto = _mapper.Map<CreateCinemaHallAppDto>(createCinemaHallApiDto);
        var createdCinemaHall = await _cinemaHallFacade.CreateAsync(createCinemaHallAppDto);
        return CreatedAtAction(nameof(GetByIdAsync), new { id = createdCinemaHall.Id },
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

    [HttpPost("{id:int}/seats")]
    [ProducesResponseType<CinemaHallSeatApiResponseDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateSeatAsync(int id, [FromBody] CreateCinemaHallSeatApiDto createSeatApiDto)
    {
        var createSeatAppDto = _mapper.Map<CreateCinemaHallSeatAppDto>(createSeatApiDto);
        var createdSeat = await _cinemaHallSeatFacade.CreateAsync(id, createSeatAppDto);
        return CreatedAtAction(nameof(GetSeatByIdAsync), new { seatId = createdSeat.Id },
            _mapper.Map<CinemaHallSeatApiResponseDto>(createdSeat));
    }
    
    [HttpPut("{id:int}/seats/{seatId:int}")]
    [ProducesResponseType<CinemaHallSeatApiResponseDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateSeatAsync(int id, int seatId, [FromBody] UpdateCinemaHallSeatApiDto updateCinemaHallSeatApiDto)
    {
        var updateSeatAppDto = _mapper.Map<UpdateCinemaHallSeatAppDto>(updateCinemaHallSeatApiDto);
        var updatedSeat = await _cinemaHallSeatFacade.UpdateAsync(id, seatId, updateSeatAppDto);
        return Ok(_mapper.Map<CinemaHallSeatApiResponseDto>(updatedSeat));
    }
    
    [HttpDelete("seats/{seatId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSeatAsync(int seatId)
    {
        await _cinemaHallSeatFacade.DeleteAsync(seatId);
        return NoContent();
    }

    [HttpGet("seats/{seatId:int}")]
    [ProducesResponseType<CinemaHallSeatApiResponseDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSeatByIdAsync(int seatId)
    {
        var seat = await _cinemaHallSeatFacade.GetByIdAsync(seatId);
        return Ok(_mapper.Map<CinemaHallSeatApiResponseDto>(seat));
    }
}