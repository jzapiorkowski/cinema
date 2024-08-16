using System.ComponentModel.DataAnnotations;

namespace Cinema.API.Movies.Dto;

public class CreateMovieActorApiDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Role { get; set; }
}