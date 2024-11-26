using Cinema.Application.Features.Persons.Interfaces;
using Cinema.Domain.Features.MovieActors.Entities;
using Cinema.Domain.Features.Movies.Entities;
using Cinema.Domain.Features.Persons.Entities;

namespace Cinema.Application.Features.Persons.Builders;

internal class PersonBuilder : IPersonBuilder
{
    private Person _person;

    public PersonBuilder()
    {
        Reset();
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

    public IPersonBuilder SetBirthDate(DateOnly birthDate)
    {
        _person.BirthDate = birthDate;
        return this;
    }

    public IPersonBuilder SetMovieActors(ICollection<MovieActor> movieActors)
    {
        _person.MovieActors = movieActors;
        return this;
    }

    public IPersonBuilder SetDirectedMovies(ICollection<Movie> directedMovies)
    {
        _person.DirectedMovies = directedMovies;
        return this;
    }

    public Person Build()
    {
        var result = _person;
        Reset();

        return result;
    }

    private void Reset()
    {
        _person = new Person();
    }
}