using AutoMapper;
using Cinema.Application.Features.Screenings.Dto;
using Cinema.Domain.Features.CinemaHalls.Entities;
using Cinema.Domain.Features.Movies.Entities;
using Cinema.Domain.Features.Screenings.Entities;
using Cinema.Domain.Features.Seats.Entities;

namespace Cinema.Application.Features.Screenings.Mappers;

internal class ScreeningProfile : Profile
{
    public ScreeningProfile()
    {
        CreateMap<Screening, ScreeningWithDetailsAppResponseDto>();
        CreateMap<Screening, ScreeningAppResponseDto>();

        CreateMap<Movie, ScreeningMovieAppResponseDto>();
        CreateMap<CinemaHall, ScreeningCinemaHallAppResponseDto>();

        CreateMap<Seat, SeatAppResponseDto>();
    }
}