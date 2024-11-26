using Cinema.Application.Features.Movies.Interfaces;
using Cinema.Domain.Features.MovieActors.Entities;
using Cinema.Domain.Features.Movies.Entities;

namespace Cinema.Application.Features.Movies.Builders;

internal class MovieBuilder : IMovieBuilder
{
    private Movie _movie;

    public MovieBuilder()
    {
        Reset();
    }

    public IMovieBuilder SetId(int id)
    {
        _movie.Id = id;
        return this;
    }

    public IMovieBuilder SetTitle(string title)
    {
        _movie.Title = title;
        return this;
    }

    public IMovieBuilder SetReleaseDate(DateOnly releaseDate)
    {
        _movie.ReleaseDate = releaseDate;
        return this;
    }

    public IMovieBuilder SetDuration(TimeSpan duration)
    {
        _movie.Duration = duration;
        return this;
    }

    public IMovieBuilder SetGenre(string genre)
    {
        _movie.Genre = genre;
        return this;
    }

    public IMovieBuilder SetDirectorId(int directorId)
    {
        _movie.DirectorId = directorId;
        return this;
    }

    public IMovieBuilder AddActors(IEnumerable<(int actorId, string role)> actors)
    {
        if (_movie.MovieActors == null)
            _movie.MovieActors = new List<MovieActor>();

        _movie.MovieActors.AddRange(
            actors.Select(actor => new MovieActor { ActorId = actor.actorId, Role = actor.role }));

        return this;
    }

    public Movie Build()
    {
        var result = _movie;
        Reset();

        return result;
    }

    private void Reset()
    {
        _movie = new Movie();
    }
}