using Cinema.Domain.MovieActors.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cinema.Infrastructure.MovieActors.ModelConfigurations;

internal class MovieActorConfiguration : IEntityTypeConfiguration<MovieActor>
{
    public void Configure(EntityTypeBuilder<MovieActor> builder)
    {
        builder.ToTable("movie_actor");
        builder.HasKey(ma => new { ma.MovieId, ma.ActorId });
        builder.Property(ma => ma.MovieId).HasColumnName("movie_id");
        builder.Property(ma => ma.ActorId).HasColumnName("actor_id");
        builder.Property(ma => ma.Role).HasColumnName("role");
        
        builder
            .HasOne(ma => ma.Movie)
            .WithMany(m => m.MovieActors)
            .HasForeignKey(ma => ma.MovieId);

        builder
            .HasOne(ma => ma.Actor)
            .WithMany(a => a.MovieActors)
            .HasForeignKey(a => a.ActorId);
    }
}