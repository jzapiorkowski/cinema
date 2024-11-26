namespace Cinema.Application.Features.Persons.Dto;

public class CreatePersonAppDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly BirthDate { get; set; }
    public IEnumerable<CreatePersonActedInAppDto> ActedIn { get; set; }
    public IEnumerable<int> DirectedMovies { get; set; }
}