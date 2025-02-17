using Cinema.Domain.Features.MovieActors.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cinema.Infrastructure.Features.MovieActors.ModelConfigurations;

internal class MovieActorConfiguration : IEntityTypeConfiguration<MovieActor>
{
    public void Configure(EntityTypeBuilder<MovieActor> builder)
    {
        builder.ToTable("movie_actor");
        builder.HasKey(ma => new { ma.MovieId, ma.ActorId });

        builder.Property(ma => ma.MovieId).HasColumnName("movie_id").IsRequired();
        builder.Property(ma => ma.ActorId).HasColumnName("actor_id").IsRequired();
        builder.Property(ma => ma.Role).HasColumnName("role").HasMaxLength(255).IsRequired();
    }
}