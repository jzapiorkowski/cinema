using Cinema.Application.Features.CinemaBuildings.Interfaces;
using Cinema.Domain.Features.CinemaBuildings.Entities;

namespace Cinema.Application.Features.CinemaBuildings.Builders;

internal class CinemaBuildingBuilder : ICinemaBuildingBuilder
{
    private readonly CinemaBuilding _cinemaBuilding;

    public CinemaBuildingBuilder()
    {
        _cinemaBuilding = new CinemaBuilding();
    }

    public ICinemaBuildingBuilder SetId(int id)
    {
        _cinemaBuilding.Id = id;
        return this;
    }

    public ICinemaBuildingBuilder SetAddress(string address)
    {
        _cinemaBuilding.Address = address;
        return this;
    }

    public CinemaBuilding Build()
    {
        return _cinemaBuilding;
    }
}