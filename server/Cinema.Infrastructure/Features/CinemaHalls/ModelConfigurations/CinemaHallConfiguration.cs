using Cinema.Domain.Features.CinemaHalls.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cinema.Infrastructure.Features.CinemaHalls.ModelConfigurations;

public class CinemaHallConfiguration : IEntityTypeConfiguration<CinemaHall>
{
    public void Configure(EntityTypeBuilder<CinemaHall> builder)
    {
        builder.ToTable("cinema_halls");
        builder.HasKey(ch => ch.Id);
    }
}