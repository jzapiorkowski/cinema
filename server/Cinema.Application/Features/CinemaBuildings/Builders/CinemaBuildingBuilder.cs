using Cinema.Application.Features.CinemaBuildings.Interfaces;
using Cinema.Domain.Features.CinemaBuildings.Entities;

namespace Cinema.Application.Features.CinemaBuildings.Builders;

internal class CinemaBuildingBuilder : ICinemaBuildingBuilder
{
    private CinemaBuilding _cinemaBuilding;

    public CinemaBuildingBuilder()
    {
        Reset();
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
        var result = _cinemaBuilding;
        Reset();

        return result;
    }

    private void Reset()
    {
        _cinemaBuilding = new CinemaBuilding();
    }
}