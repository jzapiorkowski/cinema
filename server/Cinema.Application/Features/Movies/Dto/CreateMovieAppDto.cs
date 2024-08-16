namespace Cinema.Application.Features.Movies.Dto;

public class CreateMovieAppDto
{
    public string Title { get; set; }
    public string Genre { get; set; }
    public string Director { get; set; }
    public DateTime ReleaseDate { get; set; }
    public List<CreateMovieActorAppDto> Actors { get; set; }
}