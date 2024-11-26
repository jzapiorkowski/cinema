namespace Cinema.Application.Features.Persons.Dto;

public class UpdatePersonAppDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly BirthDate { get; set; }
    public IEnumerable<UpdatePersonActedInAppDto> ActedIn { get; set; }
    public IEnumerable<int> DirectedMovies { get; set; }
}