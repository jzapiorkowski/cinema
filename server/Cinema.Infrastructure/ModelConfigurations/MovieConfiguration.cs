using Cinema.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cinema.Infrastructure.ModelConfigurations;

internal class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.ToTable("movies");
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Id).HasColumnName("id");
        builder.Property(m => m.Title).HasColumnName("title");
        builder.Property(m => m.Genre).HasColumnName("genre");
        builder.Property(m => m.Director).HasColumnName("director");
        builder.Property(m => m.ReleaseDate).HasColumnName("release_date");
    }
}
