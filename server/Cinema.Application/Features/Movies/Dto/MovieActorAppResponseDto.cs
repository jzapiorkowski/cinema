namespace Cinema.Application.Features.Movies.Dto;

public class MovieActorAppResponseDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly BirthDate { get; set; }
    public string Role { get; set; }
}