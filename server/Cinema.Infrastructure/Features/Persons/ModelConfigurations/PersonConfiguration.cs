using Cinema.Domain.Features.Persons.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cinema.Infrastructure.Features.Persons.ModelConfigurations;

internal class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable("person");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnName("id");
        builder.Property(p => p.FirstName).HasColumnName("first_name");
        builder.Property(p => p.LastName).HasColumnName("last_name");
        builder.Property(p => p.BirthDate).HasColumnName("birth_date");

        builder
            .HasMany(p => p.MovieActors)
            .WithOne(ma => ma.Actor)
            .HasForeignKey(ma => ma.ActorId);

        builder
            .HasMany(p => p.DirectedMovies)
            .WithOne(m => m.DirectedBy)
            .HasForeignKey(m => m.DirectorId);
    }
}