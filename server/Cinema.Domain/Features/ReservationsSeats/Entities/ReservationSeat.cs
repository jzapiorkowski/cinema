using Cinema.Domain.Features.Reservations.Entities;
using Cinema.Domain.Features.Seats.Entities;
using Cinema.Domain.Features.Tickets.Entities;

namespace Cinema.Domain.Features.ReservationsSeats.Entities;

public class ReservationSeat
{
    public int Id { get; set; }
    public int ReservationId { get; set; }
    public Reservation Reservation { get; set; }
    public int SeatId { get; set; }
    public Seat Seat { get; set; }
    public int TicketId { get; set; }
    public Ticket Ticket { get; set; }
}