using Cinema.Domain.Features.CinemaHalls.Entities;
using Cinema.Domain.Features.Movies.Entities;
using Cinema.Domain.Features.Reservations.Entities;

namespace Cinema.Domain.Features.Screenings.Entities;

public class Screening
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public int MovieId { get; set; }
    public virtual Movie Movie { get; set; }
    public int CinemaHallId { get; set; }
    public virtual CinemaHall CinemaHall { get; set; }
    public virtual ICollection<Reservation> Reservations { get; set; } = [];
}