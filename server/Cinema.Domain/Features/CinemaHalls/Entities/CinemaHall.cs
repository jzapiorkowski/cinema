using Cinema.Domain.Features.Screenings.Entities;

namespace Cinema.Domain.Features.CinemaHalls.Entities;

public class CinemaHall
{
    public int Id { get; set; }
    public List<Screening> Screenings { get; set; }
}