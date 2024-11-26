using Cinema.Domain.Features.Movies.Entities;

namespace Cinema.Application.Features.Movies.Interfaces;

internal interface IMovieBuilder
{
    public IMovieBuilder SetId(int id);
    public IMovieBuilder SetTitle(string title);
    public IMovieBuilder SetReleaseDate(DateOnly releaseDate);
    public IMovieBuilder SetDuration(TimeSpan duration);
    public IMovieBuilder SetGenre(string genre);
    public IMovieBuilder SetDirectorId(int directorId);
    public IMovieBuilder AddActors(IEnumerable<(int actorId, string role)> actors);
    public Movie Build();
}