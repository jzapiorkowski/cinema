namespace Cinema.Application.Features.Movies.Dto;

public class MovieWithDetailsAppResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Genre { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public TimeSpan Duration { get; set; }
    public IEnumerable<MovieActorAppResponseDto> Actors { get; set; }
    public MovieDirectorAppResponseDto Director { get; set; }
}