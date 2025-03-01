using Cinema.Domain.Features.Seats.Entities;
using Cinema.Infrastructure.Shared.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cinema.Infrastructure.Features.Seats.ModelConfigurations;

internal class SeatConfiguration : IEntityTypeConfiguration<Seat>
{
    public void Configure(EntityTypeBuilder<Seat> builder)
    {
        builder.ToTable("seat");
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).HasColumnName("id").ValueGeneratedOnAdd();

        builder.Property(s => s.Row).HasColumnName("row").HasColumnType("integer").IsRequired();
        builder.Property(s => s.Column).HasColumnName("column").HasColumnType("integer").IsRequired();
        builder.Property(s => s.CinemaHallId).HasColumnName("cinema_hall_id").IsRequired();
        builder.Property(s => s.Type).HasColumnName("type").HasConversion<string>().IsRequired();

        builder
            .HasMany(s => s.ReservationSeats)
            .WithOne(rs => rs.Seat)
            .HasForeignKey(rs => rs.SeatId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired()
            .HasConstraintName(DatabaseConstraints.FKSeatReservationSeat);

        builder.HasIndex(s => new { s.CinemaHallId, s.Row, s.Column })
            .IsUnique()
            .HasDatabaseName(DatabaseConstraints.UQSeatCinemaHallRowColumn);
    }
}