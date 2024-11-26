namespace Cinema.Application.Features.Persons.Dto;

public class PersonWithDetailsAppResponseDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly BirthDate { get; set; }
    public IEnumerable<PersonActedInAppResponseDto> ActedIn;
    public IEnumerable<MovieDirectedAppResponseDto> DirectedMovies;
}