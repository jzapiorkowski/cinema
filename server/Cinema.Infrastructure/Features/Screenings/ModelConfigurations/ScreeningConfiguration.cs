using Cinema.Domain.Features.Screenings.Entities;
using Cinema.Infrastructure.Shared.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cinema.Infrastructure.Features.Screenings.ModelConfigurations;

internal class ScreeningConfiguration : IEntityTypeConfiguration<Screening>
{
    public void Configure(EntityTypeBuilder<Screening> builder)
    {
        builder.ToTable("screening");
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).HasColumnName("id").ValueGeneratedOnAdd();

        builder.Property(s => s.StartTime).HasColumnName("start_time").HasColumnType("timestamp with time zone")
            .IsRequired();
        builder.Property(s => s.MovieId).HasColumnName("movie_id").IsRequired();
        builder.Property(s => s.CinemaHallId).HasColumnName("cinema_hall_id").IsRequired();

        builder
            .HasMany(s => s.Reservations)
            .WithOne(r => r.Screening)
            .HasForeignKey(r => r.ScreeningId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName(DatabaseConstraints.FKScreeningReservation);
    }
}