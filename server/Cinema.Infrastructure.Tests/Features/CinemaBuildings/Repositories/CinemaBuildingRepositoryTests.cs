using Cinema.Domain.Core.Interfaces;
using Cinema.Domain.Core.Pagination;
using Cinema.Domain.Features.CinemaBuildings.Entities;
using Cinema.Infrastructure.Core.Data;
using Cinema.Infrastructure.Features.CinemaBuildings.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Cinema.Infrastructure.Tests.Features.CinemaBuildings.Repositories;

public class CinemaBuildingRepositoryTests : IAsyncLifetime
{
    private ApplicationDbContext _dbContext;
    private CinemaBuildingRepository _repository;
    private readonly Mock<IAppConfiguration> _mockAppConfig;

    private readonly PostgreSqlContainer _postgreSqlContainer = new PostgreSqlBuilder()
        .WithImage("postgres:15-alpine")
        .Build();

    public CinemaBuildingRepositoryTests()
    {
        _mockAppConfig = new Mock<IAppConfiguration>();
    }

    public async Task InitializeAsync()
    {
        await _postgreSqlContainer.StartAsync();

        _mockAppConfig
            .Setup(x => x.GetDbConnectionString())
            .Returns(_postgreSqlContainer.GetConnectionString());

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseNpgsql(_postgreSqlContainer.GetConnectionString())
            .Options;

        _dbContext = new ApplicationDbContext(options, _mockAppConfig.Object);
        await _dbContext.Database.EnsureCreatedAsync();

        var loggerMock = new Mock<ILogger<CinemaBuildingRepository>>();
        _repository = new CinemaBuildingRepository(_dbContext, loggerMock.Object);
    }

    public Task DisposeAsync()
    {
        return _postgreSqlContainer.DisposeAsync().AsTask();
    }

    [Theory]
    [InlineData("Main St 1, New York, United States")]
    [InlineData("Broadway 123, Los Angeles, United States")]
    public async Task CreateAsync_ShouldAddCinemaBuildingToDatabase(string address)
    {
        var cinemaBuilding = new CinemaBuilding { Address = address };

        var result = await _repository.CreateAsync(cinemaBuilding);

        Assert.NotNull(result);
        Assert.Equal(cinemaBuilding.Address, result.Address);
        Assert.True(result.Id > 0);

        var dbCinemaBuilding = await _dbContext.CinemaBuilding.FindAsync(result.Id);
        Assert.NotNull(dbCinemaBuilding);
        Assert.Equal(cinemaBuilding.Address, dbCinemaBuilding.Address);
    }

    [Theory]
    [InlineData("Main St 1, New York, United States")]
    [InlineData("Broadway 123, Los Angeles, United States")]
    public async Task GetByIdAsync_ShouldReturnCinemaBuilding_WhenIdExists(string address)
    {
        var cinemaBuilding = new CinemaBuilding { Address = address };
        await _dbContext.CinemaBuilding.AddAsync(cinemaBuilding);
        await _dbContext.SaveChangesAsync();

        var result = await _repository.GetByIdAsync(cinemaBuilding.Id);

        Assert.NotNull(result);
        Assert.Equal(cinemaBuilding.Id, result.Id);
        Assert.Equal(cinemaBuilding.Address, result.Address);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(99)]
    public async Task GetByIdAsync_ShouldReturnNull_WhenIdDoesNotExist(int id)
    {
        var result = await _repository.GetByIdAsync(id);

        Assert.Null(result);
    }

    [Theory]
    [InlineData("Main St 1, New York, United States", "Broadway 123, Los Angeles, United States",
        "Wall St 321, New York, United States", 2, 1, 2)]
    [InlineData("Main St 1, New York, United States", "Broadway 123, Los Angeles, United States",
        "Wall St 321, New York, United States", 3, 1, 3)]
    [InlineData("Main St 1, New York, United States", "Broadway 123, Los Angeles, United States",
        "Wall St 321, New York, United States", 1, 2, 1)]
    public async Task GetAllAsync_ShouldReturnPaginatedCinemaBuildings(string address1, string address2,
        string address3, int pageSize, int pageNumber, int pageResponseSize)
    {
        var cinemaBuildings = new[]
        {
            new CinemaBuilding { Address = address1 },
            new CinemaBuilding { Address = address2 },
            new CinemaBuilding { Address = address3 }
        };
        await _dbContext.CinemaBuilding.AddRangeAsync(cinemaBuildings);
        await _dbContext.SaveChangesAsync();

        var paginationRequest = new PaginationRequest { PageNumber = pageNumber, PageSize = pageSize };

        var result = await _repository.GetAllAsync(paginationRequest);

        Assert.NotNull(result);
        Assert.Equal(pageResponseSize, result.Data.Count());
        Assert.Equal(3, result.TotalCount);
    }

    [Theory]
    [InlineData("Main St 1, New York, United States", "Broadway 123, Los Angeles, United States")]
    [InlineData("Broadway 123, Los Angeles, United States", "Wall St 321, New York, United States")]
    public async Task UpdateAsync_ShouldUpdateCinemaBuildingInDatabase(string oldAddress, string newAddress)
    {
        var cinemaBuilding = new CinemaBuilding { Address = oldAddress };
        await _dbContext.CinemaBuilding.AddAsync(cinemaBuilding);
        await _dbContext.SaveChangesAsync();

        cinemaBuilding.Address = newAddress;

        var result = await _repository.UpdateAsync(cinemaBuilding);

        Assert.NotNull(result);
        Assert.Equal(cinemaBuilding.Id, result.Id);
        Assert.Equal(newAddress, result.Address);

        var dbCinemaBuilding = await _dbContext.CinemaBuilding.FindAsync(cinemaBuilding.Id);
        Assert.NotNull(dbCinemaBuilding);
        Assert.Equal(newAddress, dbCinemaBuilding.Address);
    }
    
    [Theory]
    [InlineData("Main St 1, New York, United States")]
    [InlineData("Broadway 123, Los Angeles, United States")]
    public async Task DeleteAsync_ShouldRemoveCinemaBuildingFromDatabase(string address)
    {
        var cinemaBuilding = new CinemaBuilding { Address = address };
        await _dbContext.CinemaBuilding.AddAsync(cinemaBuilding);
        await _dbContext.SaveChangesAsync();

        await _repository.DeleteAsync(cinemaBuilding);

        var dbCinemaBuilding = await _dbContext.CinemaBuilding.FindAsync(cinemaBuilding.Id);
        Assert.Null(dbCinemaBuilding);
    }
    
    [Theory]
    [InlineData(1, 10)]
    [InlineData(1, 50)]
    public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoCinemaBuildingsExist(int pageNumber, int pageSize)
    {
        var paginationRequest = new PaginationRequest { PageNumber = pageNumber, PageSize = pageSize };

        var result = await _repository.GetAllAsync(paginationRequest);

        Assert.NotNull(result);
        Assert.Empty(result.Data);
        Assert.Equal(0, result.TotalCount);
    }
}