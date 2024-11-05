using Cinema.Domain.Features.CinemaHalls.Entities;

namespace Cinema.Domain.Features.CinemaBuildings.Entities;

public class CinemaBuilding
{
    public int Id { get; set; }
    // TODO to new entity
    public string Address { get; set; }
    public List<CinemaHall> CinemaHalls { get; set; }
}