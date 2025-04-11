using AutoMapper;
using Cinema.API.Core.Extensions;
using Cinema.API.Features.Reservations.Dto;
using Cinema.Application.Features.Reservations.Dto;
using Cinema.Application.Features.Reservations.Interfaces;
using Cinema.Domain.Core.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.API.Features.Reservations.Controllers;

[Authorize]
[ApiController]
[Route("reservations")]
[Produces("application/json")]
public class ReservationController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IReservationFacade _reservationFacade;
    private readonly IAuthorizationService _authorizationService;

    public ReservationController(IMapper mapper, IReservationFacade reservationFacade,
        IAuthorizationService authorizationService)
    {
        _mapper = mapper;
        _reservationFacade = reservationFacade;
        _authorizationService = authorizationService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateReservationApiDto reservation)
    {
        var authorizationResult = await _authorizationService
            .AuthorizeAsync(User, reservation, Core.Constants.Policies.CreateReservation);

        if (!authorizationResult.Succeeded)
            return Forbid();

        if (User.IsCustomer() && reservation.CustomerId == null)
        {
            reservation.CustomerId = User.GetUserId();
        }

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

        var authorizationResult = await _authorizationService
            .AuthorizeAsync(User, reservation, Cinema.API.Core.Constants.Policies.ManageReservations);

        if (!authorizationResult.Succeeded)
            return Forbid();

        return Ok(_mapper.Map<ReservationApiResponseDto>(reservation));
    }

    [HttpGet]
    [ProducesResponseType<PaginationResponse<ReservationApiResponseDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    // TODO get reservations by user
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
        var reservation = await _reservationFacade.GetByIdAsync(id);

        var authorizationResult = await _authorizationService
            .AuthorizeAsync(User, reservation, Cinema.API.Core.Constants.Policies.ManageReservations);

        if (!authorizationResult.Succeeded)
            return Forbid();

        var confirmedReservation = await _reservationFacade.ConfirmReservationAsync(id);
        return Ok(_mapper.Map<ReservationApiResponseDto>(confirmedReservation));
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CancelAsync(int id)
    {
        var reservation = await _reservationFacade.GetByIdAsync(id);

        var authorizationResult = await _authorizationService
            .AuthorizeAsync(User, reservation, Cinema.API.Core.Constants.Policies.ManageReservations);

        if (!authorizationResult.Succeeded)
            return Forbid();

        await _reservationFacade.CancelReservationAsync(id);
        return Ok();
    }


    [HttpPost("{reservationId:int}/seats/{seatId:int}")]
    [ProducesResponseType<ReservationApiResponseDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> AddSeatToReservationAsync(int reservationId, int seatId)
    {
        var reservation = await _reservationFacade.GetByIdAsync(reservationId);

        var authorizationResult = await _authorizationService
            .AuthorizeAsync(User, reservation, Cinema.API.Core.Constants.Policies.ManageReservations);

        if (!authorizationResult.Succeeded)
            return Forbid();

        var updatedReservation = await _reservationFacade.AddSeatToReservationAsync(reservationId, seatId);
        var reservationDto = _mapper.Map<ReservationApiResponseDto>(updatedReservation);

        return CreatedAtAction(nameof(GetByIdAsync), new { id = reservationDto.Id },
            reservationDto);
    }

    [HttpDelete("{reservationId:int}/seats/{seatId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> RemoveSeatFromReservationAsync(int reservationId, int seatId)
    {
        var reservation = await _reservationFacade.GetByIdAsync(reservationId);

        var authorizationResult = await _authorizationService
            .AuthorizeAsync(User, reservation, Cinema.API.Core.Constants.Policies.ManageReservations);

        if (!authorizationResult.Succeeded)
            return Forbid();

        await _reservationFacade.RemoveSeatFromReservationAsync(reservationId, seatId);
        return NoContent();
    }
}