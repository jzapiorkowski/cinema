using AutoMapper;
using Cinema.Application.Features.CinemaHalls.Dto;
using Cinema.Domain.Core.Pagination;
using Cinema.Domain.Features.CinemaBuildings.Entities;
using Cinema.Domain.Features.CinemaHalls.Entities;
using Cinema.Domain.Features.Screenings.Entities;
using Cinema.Domain.Features.Seats.Entities;

namespace Cinema.Application.Features.CinemaHalls.Mappers;

internal class CinemaHallProfile : Profile
{
    public CinemaHallProfile()
    {
        CreateMap<CinemaHall, CinemaHallAppResponseDto>();
        CreateMap<Screening, CinemaHallScreeningAppResponseDto>();
        CreateMap<CinemaBuilding, CinemaHallBuildingAppResponseDto>();
        CreateMap<Seat, CinemaHallSeatAppResponseDto>();

        CreateMap<PaginationResponse<CinemaHall>, PaginationResponse<CinemaHallAppResponseDto>>();

        CreateMap<CinemaHall, CinemaHallWithDetailsAppResponseDto>();
    }
}