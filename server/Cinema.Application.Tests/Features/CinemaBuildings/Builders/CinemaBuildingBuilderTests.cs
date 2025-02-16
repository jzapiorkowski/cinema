using Cinema.Application.Features.CinemaBuildings.Builders;

namespace Cinema.Application.Tests.Features.CinemaBuildings.Builders;

public class CinemaBuildingBuilderTests
{
    private readonly Mock<CinemaBuildingBuilder> _mockCinemaBuildingBuilder;
    
    public CinemaBuildingBuilderTests()
    {
        _mockCinemaBuildingBuilder = new Mock<CinemaBuildingBuilder>();
    }
    
    [Theory]
    [InlineData(1)]
    [InlineData(99)]
    public void SetId_Should_Return_Itself(int id)
    {
        var result = _mockCinemaBuildingBuilder.Object.SetId(id);
        
        Assert.Equal(_mockCinemaBuildingBuilder.Object, result);
    }
    
    [Theory]
    [InlineData("Main St 1, New York, United States")]
    [InlineData("Elm St 13, Springwood, United States")]
    public void SetAddress_Should_Return_Itself(string address)
    {
        var result = _mockCinemaBuildingBuilder.Object.SetAddress(address);
        
        Assert.Equal(_mockCinemaBuildingBuilder.Object, result);
    }
    
    [Theory]
    [InlineData(1, "Main St 1, New York, United States")]
    [InlineData(99, "Elm St 13, Springwood, United States")]
    public void Build_Should_Return_CinemaBuilding(int cinemaId, string address)
    {
        var result = _mockCinemaBuildingBuilder.Object.SetId(cinemaId).SetAddress(address).Build();
        
        Assert.Equal(cinemaId, result.Id);
        Assert.Equal(address, result.Address);
    }
}