using Cinema.Application.Features.Persons.Interfaces;
using Cinema.Domain.Features.MovieActors.Entities;
using Cinema.Domain.Features.Persons.Entities;

namespace Cinema.Application.Features.Persons.Builders;

internal class PersonBuilder : IPersonBuilder
{
    private readonly Person _person;

    public PersonBuilder()
    {
        _person = new Person();
    }

    public IPersonBuilder SetId(int id)
    {
        _person.Id = id;
        return this;
    }

    public IPersonBuilder SetFirstName(string name)
    {
        _person.FirstName = name;
        return this;
    }

    public IPersonBuilder SetLastName(string name)
    {
        _person.LastName = name;
        return this;
    }

    public IPersonBuilder SetBirthDate(DateTime birthDate)
    {
        _person.BirthDate = birthDate;
        return this;
    }

    public IPersonBuilder SetMovieActors(ICollection<MovieActor> movieActors)
    {
        _person.MovieActors = movieActors;
        return this;
    }

    public Person Build()
    {
        return _person;
    }
}