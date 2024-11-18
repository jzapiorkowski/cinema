using System.ComponentModel.DataAnnotations;

namespace Cinema.API.Features.Persons.Dto;

public class UpdatePersonActedInApiDto
{
    [Required]
    public int MovieId { get; set; }

    [Required]
    public string Role { get; set; }
}