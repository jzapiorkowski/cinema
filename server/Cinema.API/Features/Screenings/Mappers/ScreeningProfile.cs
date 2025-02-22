using AutoMapper;
using Cinema.API.Features.Screenings.Dto;
using Cinema.Application.Features.Screenings.Dto;

namespace Cinema.API.Features.Screenings.Mappers;

public class ScreeningProfile : Profile
{
    public ScreeningProfile()
    {
        CreateMap<ScreeningWithDetailsAppResponseDto, ScreeningWithDetailsApiResponseDto>();
        CreateMap<ScreeningAppResponseDto, ScreeningApiResponseDto>();

        CreateMap<CreateScreeningApiDto, CreateScreeningAppDto>();
        CreateMap<UpdateScreeningApiDto, UpdateScreeningAppDto>();

        CreateMap<ScreeningMovieAppResponseDto, ScreeningMovieApiResponseDto>();
        CreateMap<ScreeningCinemaHallAppResponseDto, ScreeningCinemaHallApiResponseDto>();

        CreateMap<SeatAppResponseDto, SeatApiResponseDto>();
    }
}