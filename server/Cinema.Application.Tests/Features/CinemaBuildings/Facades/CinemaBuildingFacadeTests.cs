using AutoMapper;
using Cinema.Application.Features.CinemaBuildings.Dto;
using Cinema.Application.Features.CinemaBuildings.Facades;
using Cinema.Application.Features.CinemaBuildings.Interfaces;
using Cinema.Domain.Core.Pagination;
using Cinema.Domain.Features.CinemaBuildings.Entities;


namespace Cinema.Application.Tests.Features.CinemaBuildings.Facades;

public class CinemaBuildingFacadeTests
{
    private readonly Mock<ICinemaBuildingService> _mockCinemaBuildingService;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<ICinemaBuildingBuilder> _mockCinemaBuildingBuilder;
    private readonly CinemaBuildingFacade _cinemaBuildingFacade;

    public CinemaBuildingFacadeTests()
    {
        _mockCinemaBuildingService = new Mock<ICinemaBuildingService>();
        _mockMapper = new Mock<IMapper>();
        _mockCinemaBuildingBuilder = new Mock<ICinemaBuildingBuilder>();
        _cinemaBuildingFacade = new CinemaBuildingFacade(
            _mockCinemaBuildingService.Object,
            _mockMapper.Object,
            _mockCinemaBuildingBuilder.Object
        );
    }

    [Theory]
    [InlineData("Main St 1, New York, United States", 1)]
    [InlineData("Broadway 123, Los Angeles, United States", 2)]
    public async Task CreateAsync_Should_Return_Mapped_CinemaBuilding(string address, int cinemaBuildingId)
    {
        var createCinemaBuildingDto = new CreateCinemaBuildingAppDto { Address = address };
        var cinemaBuilding = new CinemaBuilding { Id = cinemaBuildingId, Address = address };
        var expectedResponse = new CinemaBuildingAppResponseDto { Id = cinemaBuildingId, Address = address };

        _mockCinemaBuildingBuilder
            .Setup(b => b.SetAddress(createCinemaBuildingDto.Address))
            .Returns(_mockCinemaBuildingBuilder.Object);
        _mockCinemaBuildingBuilder
            .Setup(b => b.Build())
            .Returns(cinemaBuilding);
        _mockCinemaBuildingService
            .Setup(s => s.CreateAsync(cinemaBuilding))
            .ReturnsAsync(cinemaBuilding);
        _mockMapper
            .Setup(m => m.Map<CinemaBuildingAppResponseDto>(cinemaBuilding))
            .Returns(expectedResponse);

        var result = await _cinemaBuildingFacade.CreateAsync(createCinemaBuildingDto);

        Assert.Equal(expectedResponse, result);
    }

    [Theory]
    [InlineData("Main St 1, New York, United States", 10, 1, 1, 1)]
    [InlineData("Broadway 123, Los Angeles, United States", 15, 1, 12, 1)]
    public async Task GetAllAsync_Should_Return_Mapped_PaginationResponse(string address, int pageSize, int pageNumber,
        int cinemaBuildingId, int totalPages)
    {
        const int totalCount = 1;

        var paginationRequest = new PaginationRequest { PageNumber = pageNumber, PageSize = pageSize };
        var cinemaBuildings = new List<CinemaBuilding>
        {
            new CinemaBuilding { Id = cinemaBuildingId, Address = address }
        };
        var paginationResponse = new PaginationResponse<CinemaBuilding>
            { Data = cinemaBuildings, TotalPages = totalPages, TotalCount = totalCount, PageSize = pageSize };
        var expectedResponse = new PaginationResponse<CinemaBuildingAppResponseDto>
        {
            Data = new List<CinemaBuildingAppResponseDto>
                { new CinemaBuildingAppResponseDto { Id = cinemaBuildingId, Address = address } },
            TotalPages = totalPages, TotalCount = totalCount, PageSize = pageSize, PageNumber = pageNumber
        };

        _mockCinemaBuildingService
            .Setup(s => s.GetAllAsync(paginationRequest))
            .ReturnsAsync(paginationResponse);
        _mockMapper
            .Setup(m => m.Map<PaginationResponse<CinemaBuildingAppResponseDto>>(paginationResponse))
            .Returns(expectedResponse);

        var result = await _cinemaBuildingFacade.GetAllAsync(paginationRequest);

        Assert.Equal(expectedResponse, result);
        _mockCinemaBuildingService.Verify(s => s.GetAllAsync(paginationRequest), Times.Once);
        _mockMapper.Verify(m => m.Map<PaginationResponse<CinemaBuildingAppResponseDto>>(paginationResponse),
            Times.Once);
    }

    [Theory]
    [InlineData(1, "Main St 1, New York, United States")]
    [InlineData(42, "Elm St 13, Springwood, United States")]
    public async Task GetByIdAsync_Should_Return_Mapped_CinemaBuildingWithDetails(int cinemaBuildingId, string address)
    {
        var cinemaBuilding = new CinemaBuilding { Id = cinemaBuildingId, Address = address };
        var expectedResponse = new CinemaBuildingWithDetailsAppResponseDto { Id = cinemaBuildingId, Address = address };

        _mockCinemaBuildingService
            .Setup(s => s.GetByIdAsync(cinemaBuildingId))
            .ReturnsAsync(cinemaBuilding);
        _mockMapper
            .Setup(m => m.Map<CinemaBuildingWithDetailsAppResponseDto>(cinemaBuilding))
            .Returns(expectedResponse);

        var result = await _cinemaBuildingFacade.GetByIdAsync(cinemaBuildingId);

        Assert.Equal(expectedResponse, result);
        _mockCinemaBuildingService.Verify(s => s.GetByIdAsync(cinemaBuildingId), Times.Once);
        _mockMapper.Verify(m => m.Map<CinemaBuildingWithDetailsAppResponseDto>(cinemaBuilding), Times.Once);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(99)]
    public async Task DeleteAsync_Should_Call_Service_DeleteAsync(int cinemaBuildingId)
    {
        await _cinemaBuildingFacade.DeleteAsync(cinemaBuildingId);

        _mockCinemaBuildingService.Verify(s => s.DeleteAsync(cinemaBuildingId), Times.Once);
    }
}