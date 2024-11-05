using Cinema.Domain.Features.CinemaHalls.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cinema.Infrastructure.Features.CinemaHalls.ModelConfigurations;

public class CinemaHallConfiguration : IEntityTypeConfiguration<CinemaHall>
{
    public void Configure(EntityTypeBuilder<CinemaHall> builder)
    {
        builder.ToTable("cinema_hall");
        builder.HasKey(ch => ch.Id);
        builder.Property(ch => ch.Id).HasColumnName("id");

        builder.HasMany(ch => ch.Screenings)
            .WithOne(s => s.CinemaHall)
            .HasForeignKey(s => s.CinemaHallId);
        builder.HasOne(ch => ch.CinemaBuilding)
            .WithMany(cb => cb.CinemaHalls)
            .HasForeignKey(cb => cb.CinemaBuildingId);
    }
}