namespace Cinema.API.Features.Persons.Dto;

public class PersonWithDetailsApiResponseDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public IEnumerable<PersonActedInApiResponseDto> actedIn { get; set; }
}