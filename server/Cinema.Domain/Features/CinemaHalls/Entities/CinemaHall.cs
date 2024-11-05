using Cinema.Domain.Features.CinemaBuildings.Entities;
using Cinema.Domain.Features.Screenings.Entities;

namespace Cinema.Domain.Features.CinemaHalls.Entities;

public class CinemaHall
{
    public int Id { get; set; }
    public List<Screening> Screenings { get; set; }
    public int CinemaBuildingId { get; set; }
    public CinemaBuilding CinemaBuilding { get; set; }
}