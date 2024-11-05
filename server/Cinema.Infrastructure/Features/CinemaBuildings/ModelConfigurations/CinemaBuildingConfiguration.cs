using Cinema.Domain.Features.CinemaBuildings.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cinema.Infrastructure.Features.CinemaBuildings.ModelConfigurations;

public class CinemaBuildingConfiguration : IEntityTypeConfiguration<CinemaBuilding>
{
    public void Configure(EntityTypeBuilder<CinemaBuilding> builder)
    {
        builder.ToTable("cinema_building");
        builder.HasKey(cb => cb.Id);
        builder.Property(cb => cb.Address).HasColumnName("address");
        
        builder.HasMany(cb => cb.CinemaHalls)
            .WithOne(ch => ch.CinemaBuilding)
            .HasForeignKey(ch => ch.CinemaBuildingId);
    }
}