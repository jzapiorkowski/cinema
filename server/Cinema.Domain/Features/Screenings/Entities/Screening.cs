using Cinema.Domain.Features.CinemaHalls.Entities;
using Cinema.Domain.Features.Movies.Entities;

namespace Cinema.Domain.Features.Screenings.Entities;

public class Screening
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public int MovieId { get; set; }
    public Movie Movie { get; set; }
    public int CinemaHallId { get; set; }
    public CinemaHall CinemaHall { get; set; }
}