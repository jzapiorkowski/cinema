using Cinema.Domain.Features.Movies.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cinema.Infrastructure.Features.Movies.ModelConfigurations;

internal class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.ToTable("movie");
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Id).HasColumnName("id");
        builder.Property(m => m.Title).HasColumnName("title");
        builder.Property(m => m.Genre).HasColumnName("genre");
        builder.Property(m => m.ReleaseDate).HasColumnName("release_date").HasColumnType("date");
        builder.Property(m => m.Duration).HasColumnName("duration").HasColumnType("interval");
        builder.Property(m => m.DirectorId).HasColumnName("director_id");

        builder
            .HasMany(m => m.MovieActors)
            .WithOne(ma => ma.Movie)
            .HasForeignKey(ma => ma.MovieId);
        builder
            .HasOne(m => m.DirectedBy)
            .WithMany(p => p.DirectedMovies)
            .HasForeignKey(m => m.DirectorId)
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasMany(m => m.Screenings)
            .WithOne(s => s.Movie)
            .HasForeignKey(s => s.MovieId);
    }
}