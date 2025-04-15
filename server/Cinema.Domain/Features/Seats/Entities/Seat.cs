using Cinema.Domain.Features.CinemaHalls.Entities;
using Cinema.Domain.Features.ReservationsSeats.Entities;

namespace Cinema.Domain.Features.Seats.Entities;

public class Seat
{
    public int Id { get; set; }
    public int Row { get; set; }
    public int Column { get; set; }
    public SeatType Type { get; set; }
    
    public int CinemaHallId { get; set; }
    public virtual CinemaHall CinemaHall { get; set; }
    public virtual ICollection<ReservationSeat> ReservationSeats { get; set; } = [];
}