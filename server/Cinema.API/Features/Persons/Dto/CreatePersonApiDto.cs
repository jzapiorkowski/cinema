namespace Cinema.API.Features.Persons.Dto;

public class CreatePersonApiDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public IEnumerable<CreatePersonActedInApiDto> ActedIn { get; set; }
}