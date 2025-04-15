using Cinema.Domain.Features.MovieActors.Entities;
using Cinema.Domain.Features.Movies.Entities;

namespace Cinema.Domain.Features.Persons.Entities;

public class Person
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly BirthDate { get; set; }
    public virtual ICollection<MovieActor> MovieActors { get; set; } = [];
    public virtual ICollection<Movie> DirectedMovies { get; set; } = [];
}