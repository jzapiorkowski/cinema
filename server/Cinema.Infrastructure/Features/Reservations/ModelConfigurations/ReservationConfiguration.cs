using Cinema.Domain.Features.Reservations.Entities;
using Cinema.Infrastructure.Shared.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cinema.Infrastructure.Features.Reservations.ModelConfigurations;

internal class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.ToTable("reservation");
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).HasColumnName("id").ValueGeneratedOnAdd();

        builder.Property(r => r.Status).HasColumnName("status").HasConversion<string>().IsRequired();
        builder.Property(r => r.ScreeningId).HasColumnName("screening_id").IsRequired();

        builder
            .HasMany(r => r.ReservationSeats)
            .WithOne(rs => rs.Reservation)
            .HasForeignKey(rs => rs.ReservationId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired()
            .HasConstraintName(DatabaseConstraints.FKReservationReservationSeat);
    }
}