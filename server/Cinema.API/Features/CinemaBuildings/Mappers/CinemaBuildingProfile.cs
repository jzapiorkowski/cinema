using AutoMapper;
using Cinema.API.Features.CinemaBuildings.Dto;
using Cinema.Application.Features.CinemaBuildings.Dto;

namespace Cinema.API.Features.CinemaBuildings.Mappers;

public class CinemaBuildingProfile : Profile
{
    public CinemaBuildingProfile()
    {
        CreateMap<CinemaBuildingAppResponseDto, CinemaBuildingApiResponseDto>();
        CreateMap<CinemaBuildingWithDetailsAppResponseDto, CinemaBuildingWithDetailsApiResponseDto>();

        CreateMap<CreateCinemaBuildingApiDto, CreateCinemaBuildingAppDto>();

        CreateMap<CinemaBuildingHallAppResponseDto, CinemaBuildingHallApiResponseDto>();
    }
}