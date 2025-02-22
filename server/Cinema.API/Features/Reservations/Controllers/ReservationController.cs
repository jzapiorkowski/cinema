using AutoMapper;
using Cinema.API.Features.Reservations.Dto;
using Cinema.Application.Features.Reservations.Dto;
using Cinema.Application.Features.Reservations.Interfaces;
using Cinema.Domain.Core.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.API.Features.Reservations.Controllers;

[ApiController]
[Route("reservations")]
[Produces("application/json")]
public class ReservationController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IReservationFacade _reservationFacade;

    public ReservationController(IMapper mapper, IReservationFacade reservationFacade)
    {
        _mapper = mapper;
        _reservationFacade = reservationFacade;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateReservationApiDto reservation)
    {
        var createdReservation =
            await _reservationFacade.CreateAsync(_mapper.Map<CreateReservationAppDto>(reservation));
        var reservationApiResponse = _mapper.Map<ReservationApiResponseDto>(createdReservation);

        return CreatedAtAction(nameof(GetByIdAsync), new { id = reservationApiResponse.Id },
            reservationApiResponse);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType<ReservationApiResponseDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var reservation = await _reservationFacade.GetByIdAsync(id);
        return Ok(_mapper.Map<ReservationApiResponseDto>(reservation));
    }

    [HttpGet]
    [ProducesResponseType<PaginationResponse<ReservationApiResponseDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationRequest paginationRequest)
    {
        var reservations = await _reservationFacade.GetAllAsync(paginationRequest);
        return Ok(_mapper.Map<PaginationResponse<ReservationApiResponseDto>>(reservations));

    }
    
    [HttpPost("{id:int}/confirm")]
    [ProducesResponseType<ReservationApiResponseDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ConfirmAsync(int id)
    {
        var reservation = await _reservationFacade.ConfirmReservationAsync(id);
        return Ok(_mapper.Map<ReservationApiResponseDto>(reservation));
    }
    
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CancelAsync(int id)
    {
        await _reservationFacade.CancelReservationAsync(id);
        return Ok();
    }
    

    [HttpPost("{reservationId:int}/seats/{seatId:int}")]
    [ProducesResponseType<ReservationApiResponseDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> AddSeatToReservationAsync(int reservationId, int seatId)
    {
        var reservation = await _reservationFacade.AddSeatToReservationAsync(reservationId, seatId);
        var reservationDto = _mapper.Map<ReservationApiResponseDto>(reservation);
        return CreatedAtAction(nameof(GetByIdAsync), new { id = reservation.Id },
            reservationDto);
    }
    
    [HttpDelete("{reservationId:int}/seats/{seatId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> RemoveSeatFromReservationAsync(int reservationId, int seatId)
    {
        await _reservationFacade.RemoveSeatFromReservationAsync(reservationId, seatId);
        return NoContent();
    } 
}