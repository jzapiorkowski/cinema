namespace Cinema.Application.Features.Movies.Dto;

public class MovieWithActorAppResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Genre { get; set; }
    public string Director { get; set; }
    public DateTime ReleaseDate { get; set; }
    public ICollection<MovieActorAppResponseDto> Actors { get; set; }
}