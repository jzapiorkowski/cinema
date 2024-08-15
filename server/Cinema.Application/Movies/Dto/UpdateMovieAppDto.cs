namespace Cinema.Application.Movies.Dto;

public class UpdateMovieAppDto
{
    public string Title { get; set; }
    public string Genre { get; set; }
    public string Director { get; set; }
    public DateTime ReleaseDate { get; set; }
}