namespace Cinema.Application.Features.Persons.Dto;

public class UpdatePersonAppDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly BirthDate { get; set; }
    public List<UpdatePersonActedInAppDto> ActedIn { get; set; }
    public List<int> DirectedMovies { get; set; }
}