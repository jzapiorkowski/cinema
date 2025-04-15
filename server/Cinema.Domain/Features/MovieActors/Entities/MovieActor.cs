using Cinema.Domain.Features.Movies.Entities;
using Cinema.Domain.Features.Persons.Entities;

namespace Cinema.Domain.Features.MovieActors.Entities;

public class MovieActor
{
    public int MovieId { get; set; }
    public virtual Movie Movie { get; set; }
    public int ActorId { get; set; }
    public virtual Person Actor { get; set; }
    public string Role { get; set; }
}