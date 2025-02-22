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
    public CinemaHall CinemaHall { get; set; }
    public ICollection<ReservationSeat> ReservationSeats { get; set; } = [];
}