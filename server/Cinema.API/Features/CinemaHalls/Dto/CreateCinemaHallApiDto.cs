using System.ComponentModel.DataAnnotations;

namespace Cinema.API.Features.CinemaHalls.Dto;

public class CreateCinemaHallApiDto
{
    [Required]
    public int CinemaBuildingId { get; set; }
    
    [Required]
    public int Number { get; set; }
}