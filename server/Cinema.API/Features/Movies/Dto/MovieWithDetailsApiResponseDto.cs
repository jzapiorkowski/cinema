namespace Cinema.API.Features.Movies.Dto;

public class MovieWithDetailsApiResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Genre { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public TimeSpan Duration { get; set; }
    public IEnumerable<MovieActorApiResponseDto> Actors { get; set; }
    public MovieDirectorApiResponseDto Director { get; set; }
}