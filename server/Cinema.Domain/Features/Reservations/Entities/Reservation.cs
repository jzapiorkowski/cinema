using Cinema.Domain.Features.ReservationsSeats.Entities;
using Cinema.Domain.Features.Screenings.Entities;

namespace Cinema.Domain.Features.Reservations.Entities;

public class Reservation
{
    public int Id { get; set; }
    public ReservationStatus Status { get; set; }
    public int ScreeningId { get; set; }
    public Screening Screening { get; set; }
    public ICollection<ReservationSeat> ReservationSeats { get; set; } = [];
}