using Cinema.Domain.Features.CinemaBuildings.Entities;
using Cinema.Infrastructure.Shared.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cinema.Infrastructure.Features.CinemaBuildings.ModelConfigurations;

internal class CinemaBuildingConfiguration : IEntityTypeConfiguration<CinemaBuilding>
{
    public void Configure(EntityTypeBuilder<CinemaBuilding> builder)
    {
        builder.ToTable("cinema_building");
        builder.HasKey(cb => cb.Id);
        builder.Property(cb => cb.Id).HasColumnName("id").ValueGeneratedOnAdd();

        builder.Property(cb => cb.Address).HasColumnName("address").HasMaxLength(255).IsRequired();

        builder
            .HasMany(cb => cb.CinemaHalls)
            .WithOne(ch => ch.CinemaBuilding)
            .HasForeignKey(ch => ch.CinemaBuildingId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired()
            .HasConstraintName(DatabaseConstraints.FKCinemaBuildingCinemaHall);
    }
}