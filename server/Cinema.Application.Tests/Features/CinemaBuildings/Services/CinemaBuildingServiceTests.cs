using Cinema.Application.Features.CinemaBuildings.Services;
using Cinema.Application.Shared.Exceptions;
using Cinema.Domain.Core.Pagination;
using Cinema.Domain.Features.CinemaBuildings.Entities;
using Cinema.Domain.Features.CinemaBuildings.Repositories;
using Cinema.Domain.Shared.Interfaces;
using Microsoft.Extensions.Logging;

namespace Cinema.Application.Tests.Features.CinemaBuildings.Services;

public class CinemaBuildingServiceTests
{
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly CinemaBuildingService _cinemaBuildingService;

    public CinemaBuildingServiceTests()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _cinemaBuildingService =
            new CinemaBuildingService(Mock.Of<ILogger<CinemaBuildingService>>(), _mockUnitOfWork.Object);
    }

    [Fact]
    public async Task CreateAsync_Should_Call_Repository_CreateAsync()
    {
        const string address = "Main St 1, New York, United States";

        var cinemaBuilding = new CinemaBuilding { Address = address };
        _mockUnitOfWork
            .Setup(u => u.Repository<CinemaBuilding, ICinemaBuildingRepository>().CreateAsync(cinemaBuilding))
            .ReturnsAsync(cinemaBuilding);

        var result = await _cinemaBuildingService.CreateAsync(cinemaBuilding);

        _mockUnitOfWork.Verify(
            u => u.Repository<CinemaBuilding, ICinemaBuildingRepository>().CreateAsync(cinemaBuilding), Times.Once);
        Assert.Equal(cinemaBuilding, result);
    }

    [Fact]
    public async Task GetAllAsync_Should_Call_Repository_GetAllAsync()
    {
        const string address = "Main St 1, New York, United States";
        const int pageSize = 10;
        const int pageNumber = 1;
        const int cinemaBuildingId = 1;
        const int totalCount = 1;
        const int totalPages = 1;

        var paginationRequest = new PaginationRequest { PageNumber = pageNumber, PageSize = pageSize };
        var cinemaBuildings = new List<CinemaBuilding>
        {
            new CinemaBuilding { Id = cinemaBuildingId, Address = address }
        };
        var paginationResponse = new PaginationResponse<CinemaBuilding>
            { Data = cinemaBuildings, TotalPages = totalPages, TotalCount = totalCount, PageSize = pageSize };

        _mockUnitOfWork
            .Setup(u => u.Repository<CinemaBuilding, ICinemaBuildingRepository>()
                .GetAllAsync(paginationRequest, true, false))
            .ReturnsAsync(paginationResponse);

        var result = await _cinemaBuildingService.GetAllAsync(paginationRequest);

        _mockUnitOfWork.Verify(
            u => u.Repository<CinemaBuilding, ICinemaBuildingRepository>().GetAllAsync(paginationRequest, true, false),
            Times.Once);
        Assert.Equal(paginationResponse, result);
    }

    [Theory]
    [InlineData(1, "Main St 1, New York, United States")]
    [InlineData(42, "Elm St 13, Springwood, United States")]
    public async Task GetByIdAsync_Should_Return_CinemaBuilding_If_Found(int cinemaBuildingId, string address)
    {
        var cinemaBuilding = new CinemaBuilding { Id = cinemaBuildingId, Address = address };

        _mockUnitOfWork
            .Setup(u => u.Repository<CinemaBuilding, ICinemaBuildingRepository>()
                .GetByIdAsync(cinemaBuildingId, true, true))
            .ReturnsAsync(cinemaBuilding);

        var result = await _cinemaBuildingService.GetByIdAsync(cinemaBuildingId);

        _mockUnitOfWork.Verify(
            u => u.Repository<CinemaBuilding, ICinemaBuildingRepository>().GetByIdAsync(cinemaBuildingId, true, true),
            Times.Once);
        Assert.Equal(cinemaBuilding, result);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(99)]
    public async Task GetByIdAsync_Should_Throw_NotFoundException_If_Not_Found(int cinemaBuildingId)
    {
        _mockUnitOfWork
            .Setup(u => u.Repository<CinemaBuilding, ICinemaBuildingRepository>()
                .GetByIdAsync(cinemaBuildingId, true, true))
            .ReturnsAsync((CinemaBuilding?)null);

        await Assert.ThrowsAsync<NotFoundException>(() => _cinemaBuildingService.GetByIdAsync(cinemaBuildingId));
    }

    [Fact]
    public async Task CreateAsync_Should_Throw_AppException_If_Repository_Throws_Exception()
    {
        var cinemaBuilding = new CinemaBuilding { Address = "Main St 1" };

        _mockUnitOfWork
            .Setup(u => u.Repository<CinemaBuilding, ICinemaBuildingRepository>().CreateAsync(cinemaBuilding))
            .ThrowsAsync(new Exception("Unexpected error"));

        var exception =
            await Assert.ThrowsAsync<AppException>(() => _cinemaBuildingService.CreateAsync(cinemaBuilding));

        Assert.Equal("An error occurred while creating the cinema building.", exception.Message);
    }


    [Theory]
    [InlineData(1, "Main St 1, New York, United States")]
    [InlineData(2, "Broadway 123, Los Angeles, United States")]
    public async Task DeleteAsync_Should_Call_Repository_DeleteAsync(int cinemaBuildingId, string address)
    {
        var cinemaBuilding = new CinemaBuilding { Id = cinemaBuildingId, Address = address };

        _mockUnitOfWork
            .Setup(u => u.Repository<CinemaBuilding, ICinemaBuildingRepository>()
                .GetByIdAsync(cinemaBuildingId, true, true))
            .ReturnsAsync(cinemaBuilding);

        await _cinemaBuildingService.DeleteAsync(cinemaBuildingId);

        _mockUnitOfWork.Verify(
            u => u.Repository<CinemaBuilding, ICinemaBuildingRepository>().DeleteAsync(cinemaBuilding), Times.Once);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(99)]
    public async Task DeleteAsync_Should_Throw_NotFoundException_If_Not_Found(int cinemaBuildingId)
    {
        _mockUnitOfWork
            .Setup(u => u.Repository<CinemaBuilding, ICinemaBuildingRepository>()
                .GetByIdAsync(cinemaBuildingId, true, true))
            .ReturnsAsync((CinemaBuilding?)null);

        await Assert.ThrowsAsync<NotFoundException>(() => _cinemaBuildingService.DeleteAsync(cinemaBuildingId));
    }
}