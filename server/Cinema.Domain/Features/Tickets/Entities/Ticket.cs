using Cinema.Domain.Features.ReservationsSeats.Entities;

namespace Cinema.Domain.Features.Tickets.Entities;

public class Ticket
{
    public int Id { get; set; }
    public int ReservationSeatId { get; set; }
    public ReservationSeat ReservationSeat { get; set; }
}