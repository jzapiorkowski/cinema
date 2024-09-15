namespace Cinema.API.Features.Movies.Dto;

public class MovieApiResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Genre { get; set; }
    public DateTime ReleaseDate { get; set; }
}
