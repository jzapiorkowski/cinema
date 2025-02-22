using Cinema.Domain.Core.Pagination;
using Cinema.Domain.Features.Reservations.Entities;
using Cinema.Domain.Features.ReservationsSeats.Entities;

namespace Cinema.Application.Features.Reservations.Interfaces;

public interface IReservationService
{
    Task<Reservation> CreateAsync(Reservation reservation);
    Task<List<ReservationSeat>> AddSeatsToReservationAsync(int screeningId, List<ReservationSeat> reservationSeats);
    Task RemoveSeatFromReservationAsync(int reservationId, int seatId);
    Task<Reservation> GetByIdAsync(int id);
    Task<PaginationResponse<Reservation>> GetAllAsync(PaginationRequest paginationRequest);
    Task<Reservation> CancelReservationAsync(int reservationId);
    Task<Reservation> ConfirmReservationAsync(int id);
}