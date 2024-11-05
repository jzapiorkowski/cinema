using AutoMapper;
using Cinema.Application.Features.CinemaHalls.Dto;
using Cinema.Domain.Features.CinemaBuildings.Entities;
using Cinema.Domain.Features.CinemaHalls.Entities;
using Cinema.Domain.Features.Screenings.Entities;

namespace Cinema.Application.Features.CinemaHalls.Mappers;

public class CinemaHallProfile : Profile
{
    public CinemaHallProfile()
    {
        CreateMap<CinemaHall, CinemaHallAppResponseDto>();
        CreateMap<CinemaHall, CinemaHallWithDetailsAppResponseDto>()
            .ForMember(dest => dest.Screenings, opt => opt.MapFrom(src => src.Screenings));
        CreateMap<Screening, CinemaHallScreeningAppResponseDto>();
        CreateMap<CinemaBuilding, CinemaHallBuildingAppResponseDto>();
    }
}