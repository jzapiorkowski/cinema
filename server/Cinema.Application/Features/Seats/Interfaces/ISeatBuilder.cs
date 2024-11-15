using Cinema.Domain.Features.Seats.Entities;

namespace Cinema.Application.Features.Seats.Interfaces;

internal interface ISeatBuilder
{
    public ISeatBuilder SetId(int id);
    public ISeatBuilder SetRow(int row);
    public ISeatBuilder SetColumn(int column);
    public ISeatBuilder SetSeatType(SeatType seatType);
    public ISeatBuilder SetCinemaHallId(int cinemaHallId);
    public Seat Build();
}