using System.Linq.Expressions;
using Cinema.Domain.Features.ReservationsSeats.Entities;
using Cinema.Domain.Shared.Interfaces;

namespace Cinema.Domain.Features.ReservationsSeats.Repositories;

public interface IReservationSeatRepository : IBaseRepository<ReservationSeat>
{
    Task<bool> ExistsAsync(Expression<Func<ReservationSeat, bool>> predicate);
    Task<ReservationSeat?> GetByReservationIdAndSeatIdAsync(int reservationId, int seatId);
    Task<List<int>> GetReservedSeatIdsForScreeningAsync(int screeningId);
}