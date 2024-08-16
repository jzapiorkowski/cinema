using Cinema.Domain.MovieActors.Entities;

namespace Cinema.Domain.Persons.Entities;

public class Person
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public ICollection<MovieActor> MovieActors { get; set; }
}