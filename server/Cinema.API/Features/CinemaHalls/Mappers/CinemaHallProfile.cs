using AutoMapper;
using Cinema.API.Features.CinemaHalls.Dto;
using Cinema.Application.Features.CinemaHalls.Dto;

namespace Cinema.API.Features.CinemaHalls.Mappers;

public class CinemaHallProfile : Profile
{
    public CinemaHallProfile()
    {
        CreateMap<CinemaHallAppResponseDto, CinemaHallApiResponseDto>();
        CreateMap<CinemaHallWithDetailsAppResponseDto, CinemaHallWithDetailsApiResponseDto>();
        CreateMap<CinemaHallScreeningAppResponseDto, CinemaHallScreeningApiResponseDto>();
        CreateMap<CinemaHallBuildingAppResponseDto, CinemaHallBuildingApiResponseDto>();
        CreateMap<CinemaHallSeatAppResponseDto, CinemaHallSeatApiResponseDto>();

        CreateMap<CreateCinemaHallApiDto, CreateCinemaHallAppDto>();
        CreateMap<CreateCinemaHallApiDto, CreateCinemaHallAppDto>();
        CreateMap<CreateCinemaHallSeatApiDto, CreateCinemaHallSeatAppDto>();
    }
}