using Cinema.Domain.Features.CinemaBuildings.Entities;

namespace Cinema.Domain.Tests.Features.CinemaBuildings.Entities;

public class CinemaBuildingTests
{
    [Fact]
    public void CinemaBuilding_Should_Have_Valid_Address()
    {
        const string address = "Main St 1, New York, United States";
        var cinemaBuilding = new CinemaBuilding { Address = address };

        Assert.Equal(address, cinemaBuilding.Address);
    }
}