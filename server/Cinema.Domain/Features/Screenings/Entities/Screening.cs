using Cinema.Domain.Features.Movies.Entities;

namespace Cinema.Domain.Features.Screenings.Entities;

public class Screening
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public int MovieId { get; set; }
    public Movie Movie { get; set; }
    // TODO add room relationship
}