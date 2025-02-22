using AutoMapper;
using Cinema.Application.Features.Reservations.Dto;
using Cinema.Domain.Core.Pagination;
using Cinema.Domain.Features.CinemaHalls.Entities;
using Cinema.Domain.Features.Movies.Entities;
using Cinema.Domain.Features.Reservations.Entities;
using Cinema.Domain.Features.ReservationsSeats.Entities;
using Cinema.Domain.Features.Screenings.Entities;
using Cinema.Domain.Features.Tickets.Entities;

namespace Cinema.Application.Features.Reservations.Mappers;

internal class ReservationProfile : Profile
{
    public ReservationProfile()
    {
        CreateMap<Reservation, ReservationAppResponseDto>();
        CreateMap<Screening, ReservationScreeningAppResponseDto>();
        CreateMap<Movie, ReservationMovieAppResponseDto>();
        CreateMap<CinemaHall, ReservationCinemaHallAppResponseDto>();
        CreateMap<ReservationSeat, ReservationSeatAppResponseDto>();
        CreateMap<Ticket, ReservationTicketAppResponseDto>();
        
        CreateMap<PaginationResponse<Reservation>, PaginationResponse<ReservationAppResponseDto>>();
    }   
}