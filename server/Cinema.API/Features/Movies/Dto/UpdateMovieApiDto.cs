using System.ComponentModel.DataAnnotations;

namespace Cinema.API.Features.Movies.Dto;

public class UpdateMovieApiDto
{
    [Required]
    public string Title { get; set; }
    
    [Required]
    public string Genre { get; set; }
    
    [Required]
    public string Director { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime? ReleaseDate { get; set; }
    
    [Required]
    [MinLength(0)]
    public List<UpdateMovieActorApiDto> Actors { get; set; }
}