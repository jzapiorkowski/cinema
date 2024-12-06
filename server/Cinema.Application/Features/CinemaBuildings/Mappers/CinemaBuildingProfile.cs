using AutoMapper;
using Cinema.Application.Features.CinemaBuildings.Dto;
using Cinema.Domain.Core.Pagination;
using Cinema.Domain.Features.CinemaBuildings.Entities;
using Cinema.Domain.Features.CinemaHalls.Entities;

namespace Cinema.Application.Features.CinemaBuildings.Mappers;

internal class CinemaBuildingProfile : Profile
{
    public CinemaBuildingProfile()
    {
        CreateMap<CinemaBuilding, CinemaBuildingWithDetailsAppResponseDto>();
        CreateMap<CinemaHall, CinemaBuildingHallAppResponseDto>();

        CreateMap<CinemaBuilding, CinemaBuildingAppResponseDto>();
        CreateMap<PaginationResponse<CinemaBuilding>, PaginationResponse<CinemaBuildingAppResponseDto>>();
    }
}