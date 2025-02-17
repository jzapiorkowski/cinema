using Cinema.Domain.Features.Movies.Entities;
using Cinema.Infrastructure.Shared.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cinema.Infrastructure.Features.Movies.ModelConfigurations;

internal class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.ToTable("movie");
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Id).HasColumnName("id").ValueGeneratedOnAdd();

        builder.Property(m => m.Title).HasColumnName("title").HasMaxLength(255).IsRequired();
        builder.Property(m => m.Genre).HasColumnName("genre").HasMaxLength(50).IsRequired();
        builder.Property(m => m.ReleaseDate).HasColumnName("release_date").HasColumnType("date").IsRequired();
        builder.Property(m => m.Duration).HasColumnName("duration").HasColumnType("interval").IsRequired();
        builder.Property(m => m.DirectorId).HasColumnName("director_id").IsRequired();

        builder
            .HasMany(m => m.MovieActors)
            .WithOne(ma => ma.Movie)
            .HasForeignKey(ma => ma.MovieId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired()
            .HasConstraintName(DatabaseConstraints.FKMovieActorMovie);
        builder
            .HasMany(m => m.Screenings)
            .WithOne(s => s.Movie)
            .HasForeignKey(s => s.MovieId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired()
            .HasConstraintName(DatabaseConstraints.FKMovieScreening);
    }
}