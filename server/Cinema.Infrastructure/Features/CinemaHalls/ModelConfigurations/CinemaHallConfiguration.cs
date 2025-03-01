using Cinema.Domain.Features.CinemaHalls.Entities;
using Cinema.Infrastructure.Shared.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cinema.Infrastructure.Features.CinemaHalls.ModelConfigurations;

internal class CinemaHallConfiguration : IEntityTypeConfiguration<CinemaHall>
{
    public void Configure(EntityTypeBuilder<CinemaHall> builder)
    {
        builder.ToTable("cinema_hall");
        builder.HasKey(ch => ch.Id);
        builder.Property(ch => ch.Id).HasColumnName("id").ValueGeneratedOnAdd();

        builder.Property(ch => ch.Number).HasColumnName("number").HasColumnType("integer").IsRequired();
        builder.Property(ch => ch.CinemaBuildingId).HasColumnName("cinema_building_id").IsRequired();
        builder.Property(ch => ch.Capacity).HasColumnName("capacity").HasColumnType("integer").IsRequired();

        builder
            .HasMany(ch => ch.Screenings)
            .WithOne(s => s.CinemaHall)
            .HasForeignKey(s => s.CinemaHallId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired()
            .HasConstraintName(DatabaseConstraints.FKCinemaHallScreening);
        builder
            .HasMany(ch => ch.Seats)
            .WithOne(s => s.CinemaHall)
            .HasForeignKey(s => s.CinemaHallId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired()
            .HasConstraintName(DatabaseConstraints.FKCinemaHallSeat);

        builder.HasIndex(ch => new { ch.CinemaBuildingId, ch.Number })
            .IsUnique()
            .HasDatabaseName(DatabaseConstraints.UQCinemaHallCinemaBuildingIdNumber);
    }
}