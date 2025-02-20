using Cinema.Domain.Features.ReservationsSeats.Entities;

namespace Cinema.Application.Features.Reservations.Interfaces;

internal interface IReservationSeatBuilder
{
    IReservationSeatBuilder SetReservationId(int reservationId);
    IReservationSeatBuilder SetSeatId(int seatId);
    ReservationSeat Build();   
}