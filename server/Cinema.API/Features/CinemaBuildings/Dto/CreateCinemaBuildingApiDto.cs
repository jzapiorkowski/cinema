using System.ComponentModel.DataAnnotations;

namespace Cinema.API.Features.CinemaBuildings.Dto;

public class CreateCinemaBuildingApiDto
{
    [Required]
    public string Address { get; set; }
}