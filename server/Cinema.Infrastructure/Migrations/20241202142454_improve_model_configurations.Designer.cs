﻿// <auto-generated />
using System;
using Cinema.Infrastructure.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Cinema.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241202142454_improve_model_configurations")]
    partial class improve_model_configurations
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Cinema.Domain.Features.CinemaBuildings.Entities.CinemaBuilding", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("address");

                    b.HasKey("Id");

                    b.ToTable("cinema_building", (string)null);
                });

            modelBuilder.Entity("Cinema.Domain.Features.CinemaHalls.Entities.CinemaHall", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Capacity")
                        .HasColumnType("integer")
                        .HasColumnName("capacity");

                    b.Property<int>("CinemaBuildingId")
                        .HasColumnType("integer")
                        .HasColumnName("cinema_building_id");

                    b.Property<int>("Number")
                        .HasColumnType("integer")
                        .HasColumnName("number");

                    b.HasKey("Id");

                    b.HasIndex("CinemaBuildingId", "Number")
                        .IsUnique();

                    b.ToTable("cinema_hall", (string)null);
                });

            modelBuilder.Entity("Cinema.Domain.Features.MovieActors.Entities.MovieActor", b =>
                {
                    b.Property<int>("MovieId")
                        .HasColumnType("integer")
                        .HasColumnName("movie_id");

                    b.Property<int>("ActorId")
                        .HasColumnType("integer")
                        .HasColumnName("actor_id");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("role");

                    b.HasKey("MovieId", "ActorId");

                    b.HasIndex("ActorId");

                    b.ToTable("movie_actor", (string)null);
                });

            modelBuilder.Entity("Cinema.Domain.Features.Movies.Entities.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("DirectorId")
                        .HasColumnType("integer")
                        .HasColumnName("director_id");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("interval")
                        .HasColumnName("duration");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("genre");

                    b.Property<DateOnly>("ReleaseDate")
                        .HasColumnType("date")
                        .HasColumnName("release_date");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("title");

                    b.HasKey("Id");

                    b.HasIndex("DirectorId");

                    b.ToTable("movie", (string)null);
                });

            modelBuilder.Entity("Cinema.Domain.Features.Persons.Entities.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("BirthDate")
                        .HasColumnType("date")
                        .HasColumnName("birth_date");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("last_name");

                    b.HasKey("Id");

                    b.ToTable("person", (string)null);
                });

            modelBuilder.Entity("Cinema.Domain.Features.Screenings.Entities.Screening", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CinemaHallId")
                        .HasColumnType("integer")
                        .HasColumnName("cinema_hall_id");

                    b.Property<int>("MovieId")
                        .HasColumnType("integer")
                        .HasColumnName("movie_id");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("start_time");

                    b.HasKey("Id");

                    b.HasIndex("CinemaHallId");

                    b.HasIndex("MovieId");

                    b.ToTable("screening", (string)null);
                });

            modelBuilder.Entity("Cinema.Domain.Features.Seats.Entities.Seat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CinemaHallId")
                        .HasColumnType("integer")
                        .HasColumnName("cinema_hall_id");

                    b.Property<int>("Column")
                        .HasColumnType("integer")
                        .HasColumnName("column");

                    b.Property<int>("Row")
                        .HasColumnType("integer")
                        .HasColumnName("row");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("type");

                    b.HasKey("Id");

                    b.HasIndex("CinemaHallId", "Row", "Column")
                        .IsUnique();

                    b.ToTable("seat", (string)null);
                });

            modelBuilder.Entity("Cinema.Domain.Features.CinemaHalls.Entities.CinemaHall", b =>
                {
                    b.HasOne("Cinema.Domain.Features.CinemaBuildings.Entities.CinemaBuilding", "CinemaBuilding")
                        .WithMany("CinemaHalls")
                        .HasForeignKey("CinemaBuildingId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("CinemaBuilding");
                });

            modelBuilder.Entity("Cinema.Domain.Features.MovieActors.Entities.MovieActor", b =>
                {
                    b.HasOne("Cinema.Domain.Features.Persons.Entities.Person", "Actor")
                        .WithMany("MovieActors")
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cinema.Domain.Features.Movies.Entities.Movie", "Movie")
                        .WithMany("MovieActors")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Actor");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("Cinema.Domain.Features.Movies.Entities.Movie", b =>
                {
                    b.HasOne("Cinema.Domain.Features.Persons.Entities.Person", "DirectedBy")
                        .WithMany("DirectedMovies")
                        .HasForeignKey("DirectorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("DirectedBy");
                });

            modelBuilder.Entity("Cinema.Domain.Features.Screenings.Entities.Screening", b =>
                {
                    b.HasOne("Cinema.Domain.Features.CinemaHalls.Entities.CinemaHall", "CinemaHall")
                        .WithMany("Screenings")
                        .HasForeignKey("CinemaHallId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cinema.Domain.Features.Movies.Entities.Movie", "Movie")
                        .WithMany("Screenings")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("CinemaHall");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("Cinema.Domain.Features.Seats.Entities.Seat", b =>
                {
                    b.HasOne("Cinema.Domain.Features.CinemaHalls.Entities.CinemaHall", "CinemaHall")
                        .WithMany("Seats")
                        .HasForeignKey("CinemaHallId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CinemaHall");
                });

            modelBuilder.Entity("Cinema.Domain.Features.CinemaBuildings.Entities.CinemaBuilding", b =>
                {
                    b.Navigation("CinemaHalls");
                });

            modelBuilder.Entity("Cinema.Domain.Features.CinemaHalls.Entities.CinemaHall", b =>
                {
                    b.Navigation("Screenings");

                    b.Navigation("Seats");
                });

            modelBuilder.Entity("Cinema.Domain.Features.Movies.Entities.Movie", b =>
                {
                    b.Navigation("MovieActors");

                    b.Navigation("Screenings");
                });

            modelBuilder.Entity("Cinema.Domain.Features.Persons.Entities.Person", b =>
                {
                    b.Navigation("DirectedMovies");

                    b.Navigation("MovieActors");
                });
#pragma warning restore 612, 618
        }
    }
}
