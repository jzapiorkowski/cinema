using System.ComponentModel.DataAnnotations;

namespace Cinema.API.Features.CinemaHalls.Dto;

public class UpdateCinemaHallApiDto
{
    [Required]
    public int CinemaBuildingId { get; set; }
    
    [Required]
    [Range(1, int.MaxValue)]
    public int Number { get; set; }
    
    [Required]
    [Range(1, int.MaxValue)]
    public int Capacity { get; set; }
}