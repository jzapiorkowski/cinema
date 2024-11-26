namespace Cinema.API.Features.Persons.Dto;

public class PersonWithDetailsApiResponseDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly BirthDate { get; set; }
    public IEnumerable<PersonActedInApiResponseDto> ActedIn { get; set; }
    public IEnumerable<MovieDirectedApiResponseDto> DirectedMovies { get; set; }
}