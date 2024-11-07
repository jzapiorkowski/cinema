using Cinema.Domain.Features.CinemaBuildings.Entities;

namespace Cinema.Application.Features.CinemaBuildings.Interfaces;

internal interface ICinemaBuildingBuilder
{
    public ICinemaBuildingBuilder SetId(int id);
    public ICinemaBuildingBuilder SetAddress(string address);
    public CinemaBuilding Build();
}