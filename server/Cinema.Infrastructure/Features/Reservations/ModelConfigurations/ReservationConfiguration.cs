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

        builder.Property(r => r.Status).HasColumnName("status").HasConversion<string>()
            .HasDefaultValue(ReservationStatus.RESERVED).IsRequired();
        builder.Property(r => r.CanceledAt).HasColumnName("canceled_at").HasColumnType("timestamp with time zone")
            .IsRequired(false);

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