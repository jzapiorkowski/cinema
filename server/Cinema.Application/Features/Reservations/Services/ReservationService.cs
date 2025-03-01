using Cinema.Application.Features.Reservations.Exceptions;
using Cinema.Application.Features.Reservations.Interfaces;
using Cinema.Application.Features.Screenings.Interfaces;
using Cinema.Application.Shared.Exceptions;
using Cinema.Domain.Core.Pagination;
using Cinema.Domain.Features.Reservations.Entities;
using Cinema.Domain.Features.Reservations.Repositories;
using Cinema.Domain.Features.ReservationsSeats.Entities;
using Cinema.Domain.Features.ReservationsSeats.Repositories;
using Cinema.Domain.Features.Tickets.Entities;
using Cinema.Domain.Features.Tickets.Repositories;
using Cinema.Domain.Shared.Exceptions;
using Cinema.Domain.Shared.Interfaces;
using Microsoft.Extensions.Logging;

namespace Cinema.Application.Features.Reservations.Services;

internal class ReservationService : IReservationService
{
    private readonly ILogger<ReservationService> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IScreeningService _screeningService;
    private readonly ITicketBuilder _ticketBuilder;

    public ReservationService(ILogger<ReservationService> logger, IUnitOfWork unitOfWork,
        IScreeningService screeningService, ITicketBuilder ticketBuilder)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _screeningService = screeningService;
        _ticketBuilder = ticketBuilder;
    }

    public Task<Reservation> CreateAsync(Reservation reservation)
    {
        try
        {
            reservation.Reserve();
            return _unitOfWork.Repository<Reservation, IReservationRepository>().CreateAsync(reservation);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while creating reservation");
            throw new AppException($"An error occurred while creating reservation");
        }
    }

    public async Task<List<ReservationSeat>> AddSeatsToReservationAsync(int screeningId,
        List<ReservationSeat> reservationSeats)
    {
        var reservation = await GetByIdAsync(reservationSeats[0].ReservationId);

        if (!reservation.CanAddSeats())
        {
            throw new SeatsAddingException($"Cannot add seats to reservation {reservation.Id}.");
        }

        foreach (var seat in reservationSeats)
        {
            await ValidateSeatAvailabilityAsync(screeningId, seat.SeatId);
        }

        try
        {
            return await _unitOfWork.Repository<ReservationSeat, IReservationSeatRepository>()
                .CreateManyAsync(reservationSeats);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while adding seats to reservation with id {id}",
                reservationSeats[0].ReservationId);
            throw new AppException(
                $"An error occurred while adding seats to reservation with id {reservationSeats[0].ReservationId}", e);
        }
    }

    public async Task RemoveSeatFromReservationAsync(int reservationId, int seatId)
    {
        var reservation = await GetByIdAsync(reservationId);

        if (!reservation.CanRemoveSeat())
        {
            throw new SeatsRemovingException($"Cannot remove seat from reservation {reservationId}.");
        }

        try
        {
            var reservationSeat = await _unitOfWork.Repository<ReservationSeat, IReservationSeatRepository>()
                .GetByReservationIdAndSeatIdAsync(reservation.Id, seatId);

            if (reservationSeat == null)
            {
                throw new NotFoundException("reservation seat", seatId);
            }

            await _unitOfWork.Repository<ReservationSeat, IReservationSeatRepository>().DeleteAsync(reservationSeat);
        }
        catch (NotFoundException)
        {
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while removing seat from reservation with id {id}", reservationId);
            throw new AppException($"An error occurred while removing seat from reservation with id {reservationId}",
                e);
        }
    }

    public async Task<Reservation> GetByIdAsync(int id)
    {
        return await GetByIdAsync(id, true, true);
    }

    public async Task<PaginationResponse<Reservation>> GetAllAsync(PaginationRequest paginationRequest)
    {
        try
        {
            return await _unitOfWork.Repository<Reservation, IReservationRepository>()
                .GetAllAsync(paginationRequest, includeAllRelations: true);
        }
        catch (InvalidSortByException)
        {
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while retrieving all reservations");
            throw new AppException($"An error occurred while retrieving all reservations");
        }
    }

    public async Task<Reservation> CancelReservationAsync(int reservationId)
    {
        var reservation = await GetByIdAsync(reservationId);

        try
        {
            reservation.Cancel();
            foreach (var reservationSeat in reservation.ReservationSeats)
            {
                reservationSeat.MarkAsDeleted();
                reservationSeat.Ticket.MarkAsDeleted();
            }

            return await _unitOfWork.Repository<Reservation, IReservationRepository>().UpdateAsync(reservation);
        }
        catch (InvalidOperationException e)
        {
            _logger.LogError(e, "An error occurred while canceling reservation with id {id}", reservationId);
            throw new ReservationCancellingException(e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while canceling reservation with id {id}", reservationId);
            throw new AppException($"An error occurred while canceling reservation with id {reservationId}", e);
        }
    }

    public async Task<Reservation> ConfirmReservationAsync(int id)
    {
        var reservation = await GetByIdAsync(id);

        try
        {
            reservation.Confirm();
            var tickets = reservation.ReservationSeats.Select(rs => _ticketBuilder.SetReservationSeatId(rs.Id).Build())
                .ToList();
            await _unitOfWork.Repository<Ticket, ITicketRepository>().CreateManyAsync(tickets);
            return await _unitOfWork.Repository<Reservation, IReservationRepository>().UpdateAsync(reservation);
        }
        catch (InvalidOperationException e)
        {
            _logger.LogError(e, "An error occurred while confirming reservation with id {id}", id);
            throw new ReservationConfirmingException(e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while confirming reservation with id {id}", id);
            throw new AppException($"An error occurred while confirming reservation with id {id}", e);
        }
    }

    private async Task<Reservation> GetByIdAsync(int id, bool asNoTracking, bool includeAllRelations)
    {
        try
        {
            var reservation = await _unitOfWork.Repository<Reservation, IReservationRepository>()
                .GetByIdAsync(id, asNoTracking, includeAllRelations);

            if (reservation == null)
            {
                throw new NotFoundException("reservation", id);
            }

            return reservation;
        }
        catch (NotFoundException)
        {
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while retrieving reservation with id {id}", id);
            throw new AppException($"An error occurred while retrieving reservation with id {id}", e);
        }
    }

    private async Task ValidateSeatAvailabilityAsync(int screeningId, int seatId)
    {
        if (!await _screeningService.IsSeatAvailableAsync(screeningId, seatId))
        {
            throw new SeatAlreadyOccupiedException(seatId, screeningId);
        }
    }
}