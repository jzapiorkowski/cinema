using Cinema.Domain.Features.Screenings.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cinema.Infrastructure.Features.Screenings.ModelConfigurations;

public class ScreeningConfiguration : IEntityTypeConfiguration<Screening>
{
    public void Configure(EntityTypeBuilder<Screening> builder)
    {
        builder.ToTable("screening");
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).HasColumnName("id");
        builder.Property(s => s.StartTime).HasColumnName("start_time").HasColumnType("timestamp with time zone");
        builder.Property(s => s.MovieId).HasColumnName("movie_id");
        builder.Property(s => s.CinemaHallId).HasColumnName("cinema_hall_id");

        builder.HasOne(s => s.Movie)
            .WithMany(m => m.Screenings)
            .HasForeignKey(s => s.MovieId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(s => s.CinemaHall)
            .WithMany(ch => ch.Screenings)
            .HasForeignKey(s => s.CinemaHallId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}