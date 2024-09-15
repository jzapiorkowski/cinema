using System.ComponentModel.DataAnnotations;

namespace Cinema.API.Features.Persons.Dto;

public class CreatePersonApiDto
{
    [Required]
    public string FirstName { get; set; }
    
    [Required]
    public string LastName { get; set; }
    
    [Required]
    public DateTime BirthDate { get; set; }
    
    [Required]
    public IEnumerable<CreatePersonActedInApiDto> ActedIn { get; set; }
    
    [Required]
    public IEnumerable<int> DirectedMovies { get; set; }
}