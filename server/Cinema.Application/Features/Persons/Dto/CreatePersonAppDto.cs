namespace Cinema.Application.Features.Persons.Dto;

public class CreatePersonAppDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly BirthDate { get; set; }
    public List<CreatePersonActedInAppDto> ActedIn { get; set; }
    public List<int> DirectedMovies { get; set; }
}