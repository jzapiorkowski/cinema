using AutoMapper;
using Cinema.Application.Features.CinemaBuildings.Dto;
using Cinema.Domain.Features.CinemaBuildings.Entities;
using Cinema.Domain.Features.CinemaHalls.Entities;

namespace Cinema.Application.Features.CinemaBuildings.Mappers;

public class CinemaBuildingProfile : Profile
{
    public CinemaBuildingProfile()
    {
        CreateMap<CinemaBuilding, CinemaBuildingWithDetailsAppResponseDto>();
        CreateMap<CinemaBuilding, CinemaBuildingAppResponseDto>();

        CreateMap<CinemaHall, CinemaBuildingHallAppResponseDto>();
    }
}