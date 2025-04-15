using Cinema.Domain.Features.MovieActors.Entities;
using Cinema.Domain.Features.Persons.Entities;
using Cinema.Domain.Features.Screenings.Entities;

namespace Cinema.Domain.Features.Movies.Entities;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Genre { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public TimeSpan Duration { get; set; }
    public virtual ICollection<MovieActor> MovieActors { get; set; } = [];
    public virtual Person DirectedBy { get; set; }
    public int DirectorId { get; set; }
    public virtual ICollection<Screening> Screenings { get; set; } = [];
}