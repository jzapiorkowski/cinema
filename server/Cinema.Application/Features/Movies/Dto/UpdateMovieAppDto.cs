namespace Cinema.Application.Features.Movies.Dto;

public class UpdateMovieAppDto
{
    public string Title { get; set; }
    public string Genre { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public TimeSpan Duration { get; set; }
    public List<UpdateMovieActorAppDto> Actors { get; set; }
    public int DirectorId { get; set; }
}