namespace Cinema.API.Movies.Dto;

public class MovieWithActorsApiResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Genre { get; set; }
    public string Director { get; set; }
    public DateTime ReleaseDate { get; set; }
    public ICollection<MovieActorApiResponseDto> Actors { get; set; }
}