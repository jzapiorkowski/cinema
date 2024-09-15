namespace Cinema.Application.Features.Movies.Dto;

public class MovieAppResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Genre { get; set; }
    public DateTime ReleaseDate { get; set; }
    public TimeSpan Duration { get; set; }
}