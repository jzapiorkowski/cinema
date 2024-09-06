namespace Cinema.Application.Features.Persons.Dto;

public class CreatePersonAppDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public IEnumerable<CreatePersonActedInAppDto> ActedIn { get; set; }
}