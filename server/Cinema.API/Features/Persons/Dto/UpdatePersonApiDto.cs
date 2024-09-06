namespace Cinema.API.Features.Persons.Dto;

public class UpdatePersonApiDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public IEnumerable<UpdatePersonActedInApiDto> ActedIn { get; set; }
}