using Cinema.Domain.Features.Reservations.Entities;
using Cinema.Domain.Features.Seats.Entities;
using Cinema.Domain.Features.Tickets.Entities;

namespace Cinema.Domain.Features.ReservationsSeats.Entities;

public class ReservationSeat
{
    public int Id { get; set; }
    public int ReservationId { get; set; }
    public virtual Reservation Reservation { get; set; }
    public bool IsDeleted { get; set; }
    public int SeatId { get; set; }
    public virtual Seat Seat { get; set; }
    public int TicketId { get; set; }
    public virtual Ticket Ticket { get; set; }

    public void MarkAsDeleted()
    {
        IsDeleted = true;
        Ticket?.MarkAsDeleted();
    }
}