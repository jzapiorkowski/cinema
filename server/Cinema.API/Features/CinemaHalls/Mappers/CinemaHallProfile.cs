using AutoMapper;
using Cinema.API.Features.CinemaHalls.Dto;
using Cinema.Application.Features.CinemaHalls.Dto;

namespace Cinema.API.Features.CinemaHalls.Mappers;

public class CinemaHallProfile : Profile
{
    public CinemaHallProfile()
    {
        CreateMap<CinemaHallAppResponseDto, CinemaHallApiResponseDto>();
        CreateMap<CreateCinemaHallApiDto, CreateCinemaHallAppDto>();
        CreateMap<CinemaHallWithDetailsAppResponseDto, CinemaHallWithDetailsApiResponseDto>();
        CreateMap<CinemaHallScreeningAppResponseDto, CinemaHallScreeningApiResponseDto>();
        CreateMap<CinemaHallBuildingAppResponseDto, CinemaHallBuildingApiResponseDto>();
        CreateMap<CreateCinemaHallApiDto, CreateCinemaHallAppDto>();
    }
}