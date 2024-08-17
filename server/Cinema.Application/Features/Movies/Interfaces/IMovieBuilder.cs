using Cinema.Domain.Features.Movies.Entities;

namespace Cinema.Application.Features.Movies.Interfaces;

internal interface IMovieBuilder
{
    public IMovieBuilder SetId(int id);
    public IMovieBuilder SetTitle(string title);
    public IMovieBuilder SetReleaseDate(DateTime releaseDate);
    public IMovieBuilder SetGenre(string genre);
    public IMovieBuilder SetDirector(string director);
    public IMovieBuilder AddActors(IEnumerable<(int actorId, string role)> actors);
    public Movie Build();
}