namespace Cinema.Application.Features.Persons.Dto;

public class MovieDirectedAppResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Genre { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public TimeSpan Duration { get; set; }
}