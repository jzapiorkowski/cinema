using Cinema.Domain.Features.Tickets.Entities;
using Cinema.Infrastructure.Shared.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cinema.Infrastructure.Features.Tickets.ModelConfigurations;

internal class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.ToTable("ticket");
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).HasColumnName("id").ValueGeneratedOnAdd();
        builder.Property(r => r.ReservationSeatId).HasColumnName("reservation_seat_id").IsRequired();

        builder
            .HasOne(t => t.ReservationSeat)
            .WithOne(rs => rs.Ticket)
            .HasForeignKey<Ticket>(t => t.ReservationSeatId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired()
            .HasConstraintName(DatabaseConstraints.FKTicketReservationSeat);
    }
}