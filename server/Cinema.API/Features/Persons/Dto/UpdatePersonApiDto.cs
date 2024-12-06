using System.ComponentModel.DataAnnotations;

namespace Cinema.API.Features.Persons.Dto;

public class UpdatePersonApiDto
{
    [Required]
    public string FirstName { get; set; }
    
    [Required]
    public string LastName { get; set; }

    [Required]
    public DateOnly BirthDate { get; set; }

    [Required]
    public List<UpdatePersonActedInApiDto> ActedIn { get; set; }
    
    [Required]
    public List<int> DirectedMovies { get; set; }
}