using Cinema.Domain.Features.ReservationsSeats.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cinema.Infrastructure.Features.ReservationSeats.ModelConfigurations;

internal class ReservationSeatConfiguration : IEntityTypeConfiguration<ReservationSeat>
{
    public void Configure(EntityTypeBuilder<ReservationSeat> builder)
    {
        builder.ToTable("reservation_seat");
        builder.HasKey(rs => rs.Id);
        builder.Property(rs => rs.Id).HasColumnName("id").ValueGeneratedOnAdd();

        builder.Property(rs => rs.ReservationId).HasColumnName("reservation_id").IsRequired();
        builder.Property(rs => rs.SeatId).HasColumnName("seat_id").IsRequired();
        builder.Property(rs => rs.TicketId).HasColumnName("ticket_id").IsRequired();
    }
}