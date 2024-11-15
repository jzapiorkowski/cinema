using Cinema.Application.Features.Seats.Interfaces;
using Cinema.Domain.Features.Seats.Entities;

namespace Cinema.Application.Features.Seats.Builders;

internal class SeatBuilder : ISeatBuilder
{
    private Seat _seat;

    public SeatBuilder()
    {
        Reset();
    }

    public ISeatBuilder SetId(int id)
    {
        _seat.Id = id;
        return this;
    }

    public ISeatBuilder SetRow(int row)
    {
        _seat.Row = row;
        return this;
    }

    public ISeatBuilder SetColumn(int column)
    {
        _seat.Column = column;
        return this;
    }


    public ISeatBuilder SetSeatType(SeatType seatType)
    {
        _seat.Type = seatType;
        return this;
    }

    public ISeatBuilder SetCinemaHallId(int cinemaHallId)
    {
        _seat.CinemaHallId = cinemaHallId;
        return this;
    }

    public Seat Build()
    {
        var result = _seat;
        Reset();
        
        return result;
    }

    private void Reset()
    {
        _seat = new Seat();
    }
}