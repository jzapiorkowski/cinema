using AutoMapper;
using Cinema.Application.Features.CinemaHalls.Dto;
using Cinema.Domain.Features.CinemaHalls.Entities;

namespace Cinema.Application.Features.CinemaHalls.Mappers;

public class CinemaHallProfile : Profile
{
    public CinemaHallProfile()
    {
        CreateMap<CinemaHall, CinemaHallAppResponseDto>();
    }
}