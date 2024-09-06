using Cinema.Domain.Features.MovieActors.Entities;
using Cinema.Domain.Features.Persons.Entities;

namespace Cinema.Application.Features.Persons.Interfaces;

internal interface IPersonBuilder
{
    public IPersonBuilder SetId(int id);
    public IPersonBuilder SetFirstName(string name);
    public IPersonBuilder SetLastName(string name);
    public IPersonBuilder SetBirthDate(DateTime birthDate);
    public IPersonBuilder SetMovieActors(ICollection<MovieActor> movieActors);
    public Person Build();
}