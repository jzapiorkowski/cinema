using Cinema.Domain.Features.ReservationsSeats.Entities;
using Cinema.Domain.Features.Screenings.Entities;

namespace Cinema.Domain.Features.Reservations.Entities;

public class Reservation
{
    public int Id { get; set; }
    public ReservationStatus Status { get; set; }
    public int ScreeningId { get; set; }
    public DateTime? CanceledAt { get; set; }
    public Screening Screening { get; set; }
    public ICollection<ReservationSeat> ReservationSeats { get; set; } = [];

    public void Cancel()
    {
        if (Status == ReservationStatus.CANCELED)
        {
            throw new InvalidOperationException("Reservation is already in canceled.");
        }

        if (Status == ReservationStatus.CONFIRMED)
        {
            throw new InvalidOperationException("Cannot cancel a confirmed reservation.");
        }

        if (Screening.StartTime < DateTime.UtcNow)
        {
            throw new InvalidOperationException(
                "Cannot cancel a reservation for a screening that has already started.");
        }

        Status = ReservationStatus.CANCELED;
        CanceledAt = DateTime.UtcNow;
    }

    public void Reserve()
    {
        if (Status == ReservationStatus.RESERVED)
        {
            throw new InvalidOperationException("Reservation is already in RESERVED state.");
        }

        Status = ReservationStatus.RESERVED;
    }

    public void Confirm()
    {
        if (Status == ReservationStatus.CONFIRMED)
        {
            throw new InvalidOperationException("Reservation is already in CONFIRMED state.");
        }

        Status = ReservationStatus.CONFIRMED;
    }

    public bool CanAddSeats()
    {
        return Status == ReservationStatus.RESERVED && Screening.StartTime > DateTime.UtcNow;
    }

    public bool CanRemoveSeat()
    {
        return Status == ReservationStatus.RESERVED && Screening.StartTime > DateTime.UtcNow &&
               ReservationSeats.Count > 1;
    }
}