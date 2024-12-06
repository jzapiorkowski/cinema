using System.ComponentModel.DataAnnotations;

namespace Cinema.API.Features.Screenings.Dto;

public class CreateScreeningApiDto
{
    [Required]
    [DataType(DataType.DateTime)]
    public DateTime StartTime { get; set; }

    [Required]
    public int MovieId { get; set; }

    [Required] 
    public int CinemaHallId { get; set; }
}