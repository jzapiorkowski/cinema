using Cinema.Domain.Features.CinemaBuildings.Entities;
using Cinema.Domain.Features.Screenings.Entities;
using Cinema.Domain.Features.Seats.Entities;

namespace Cinema.Domain.Features.CinemaHalls.Entities;

public class CinemaHall
{
    public int Id { get; set; }
    public int Number { get; set; }
    public int Capacity { get; set; }

    public List<Seat> Seats { get; set; } = [];
    public List<Screening> Screenings { get; set; } = [];
    public int CinemaBuildingId { get; set; }
    public CinemaBuilding CinemaBuilding { get; set; }

    public bool CanAddSeats(int additionalSeats)
    {
        return (Seats.Count + additionalSeats) <= Capacity;
    }
    
    public bool CanChangeCapacity(int newCapacity)
    {
        return Seats.Count <= newCapacity;
    }
}