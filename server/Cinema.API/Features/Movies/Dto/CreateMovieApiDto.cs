using System.ComponentModel.DataAnnotations;

namespace Cinema.API.Features.Movies.Dto;

public class CreateMovieApiDto
{
    [Required]
    public string Title { get; set; }

    [Required]
    public string Genre { get; set; }

    [Required]
    public int DirectorId { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime? ReleaseDate { get; set; }

    [Required]
    public TimeSpan Duration { get; set; }

    [Required]
    [MinLength(0)]
    public List<CreateMovieActorApiDto> Actors { get; set; }
}