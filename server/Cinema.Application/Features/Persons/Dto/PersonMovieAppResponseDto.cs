namespace Cinema.Application.Features.Persons.Dto;

public class PersonMovieAppResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Genre { get; set; }
    public string Director { get; set; }
    public DateTime ReleaseDate { get; set; }
    public TimeSpan Duration { get; set; }
}