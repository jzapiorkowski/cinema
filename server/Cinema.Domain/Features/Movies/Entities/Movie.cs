using Cinema.Domain.Features.MovieActors.Entities;

namespace Cinema.Domain.Features.Movies.Entities;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Genre { get; set; }
    public string Director { get; set; }
    public DateTime ReleaseDate { get; set; }
    public ICollection<MovieActor> MovieActors { get; set; }
}