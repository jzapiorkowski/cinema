using AutoMapper;
using Cinema.Application.Features.CinemaHalls.Dto;
using Cinema.Domain.Core.Pagination;
using Cinema.Domain.Features.CinemaBuildings.Entities;
using Cinema.Domain.Features.CinemaHalls.Entities;
using Cinema.Domain.Features.Screenings.Entities;
using Cinema.Domain.Features.Seats.Entities;

namespace Cinema.Application.Features.CinemaHalls.Mappers;

public class CinemaHallProfile : Profile
{
    public CinemaHallProfile()
    {
        CreateMap<CinemaHall, CinemaHallAppResponseDto>();
        CreateMap<PaginationResponse<CinemaHall>, PaginationResponse<CinemaHallAppResponseDto>>()
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data.Select(ch => new CinemaHallAppResponseDto
            {
                Id = ch.Id,
                Number = ch.Number,
                Capacity = ch.Capacity
            })));
        
        CreateMap<CinemaHall, CinemaHallWithDetailsAppResponseDto>()
            .ForMember(dest => dest.Screenings, opt => opt.MapFrom(src => src.Screenings));
        CreateMap<Screening, CinemaHallScreeningAppResponseDto>();
        CreateMap<CinemaBuilding, CinemaHallBuildingAppResponseDto>();
        CreateMap<Seat, CinemaHallSeatAppResponseDto>();
    }
}