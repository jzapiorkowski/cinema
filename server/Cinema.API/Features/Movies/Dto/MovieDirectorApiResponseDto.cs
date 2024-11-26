namespace Cinema.API.Features.Movies.Dto;

public class MovieDirectorApiResponseDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly BirthDate { get; set; }
}