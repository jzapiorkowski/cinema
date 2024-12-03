using AutoMapper;
using Cinema.API.Features.CinemaHalls.Dto;
using Cinema.Application.Features.CinemaHalls.Dto;
using Cinema.Domain.Core.Pagination;

namespace Cinema.API.Features.CinemaHalls.Mappers;

public class CinemaHallProfile : Profile
{
    public CinemaHallProfile()
    {
        CreateMap<CinemaHallAppResponseDto, CinemaHallApiResponseDto>();
        CreateMap<PaginationResponse<CinemaHallAppResponseDto>, PaginationResponse<CinemaHallApiResponseDto>>()
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data));

        CreateMap<CinemaHallWithDetailsAppResponseDto, CinemaHallWithDetailsApiResponseDto>();
        CreateMap<CinemaHallScreeningAppResponseDto, CinemaHallScreeningApiResponseDto>();
        CreateMap<CinemaHallBuildingAppResponseDto, CinemaHallBuildingApiResponseDto>();
        CreateMap<CinemaHallSeatAppResponseDto, CinemaHallSeatApiResponseDto>();

        CreateMap<CreateCinemaHallApiDto, CreateCinemaHallAppDto>();
        CreateMap<CreateCinemaHallApiDto, CreateCinemaHallAppDto>();
        CreateMap<CreateCinemaHallSeatApiDto, CreateCinemaHallSeatAppDto>();

        CreateMap<UpdateCinemaHallApiDto, UpdateCinemaHallAppDto>();
        CreateMap<UpdateCinemaHallSeatApiDto, UpdateCinemaHallSeatAppDto>();
    }
}