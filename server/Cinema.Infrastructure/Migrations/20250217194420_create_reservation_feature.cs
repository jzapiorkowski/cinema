using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Cinema.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class create_reservation_feature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cinema_hall_cinema_building_id",
                table: "cinema_hall");

            migrationBuilder.DropForeignKey(
                name: "FK_screening_cinema_hall_id",
                table: "screening");

            migrationBuilder.DropForeignKey(
                name: "FK_screening_movie_id",
                table: "screening");

            migrationBuilder.DropForeignKey(
                name: "FK_seat_cinema_hall_id",
                table: "seat");

            migrationBuilder.CreateTable(
                name: "reservation",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    status = table.Column<string>(type: "text", nullable: false),
                    screening_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reservation", x => x.id);
                    table.ForeignKey(
                        name: "FK_screening_reservation_id",
                        column: x => x.screening_id,
                        principalTable: "screening",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "reservation_seat",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    reservation_id = table.Column<int>(type: "integer", nullable: false),
                    seat_id = table.Column<int>(type: "integer", nullable: false),
                    ticket_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reservation_seat", x => x.id);
                    table.ForeignKey(
                        name: "FK_reservation_reservation_seat_id",
                        column: x => x.reservation_id,
                        principalTable: "reservation",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_seat_reservation_seat_id",
                        column: x => x.seat_id,
                        principalTable: "seat",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ticket",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    reservation_seat_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ticket", x => x.id);
                    table.ForeignKey(
                        name: "FK_ticket_reservation_seat_id",
                        column: x => x.reservation_seat_id,
                        principalTable: "reservation_seat",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_reservation_screening_id",
                table: "reservation",
                column: "screening_id");

            migrationBuilder.CreateIndex(
                name: "IX_reservation_seat_reservation_id",
                table: "reservation_seat",
                column: "reservation_id");

            migrationBuilder.CreateIndex(
                name: "IX_reservation_seat_seat_id",
                table: "reservation_seat",
                column: "seat_id");

            migrationBuilder.CreateIndex(
                name: "IX_ticket_reservation_seat_id",
                table: "ticket",
                column: "reservation_seat_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_cinema_building_cinema_hall_id",
                table: "cinema_hall",
                column: "cinema_building_id",
                principalTable: "cinema_building",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_cinema_hall_screening_id",
                table: "screening",
                column: "cinema_hall_id",
                principalTable: "cinema_hall",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_movie_screening_id",
                table: "screening",
                column: "movie_id",
                principalTable: "movie",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_cinema_hall_seat_id",
                table: "seat",
                column: "cinema_hall_id",
                principalTable: "cinema_hall",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cinema_building_cinema_hall_id",
                table: "cinema_hall");

            migrationBuilder.DropForeignKey(
                name: "FK_cinema_hall_screening_id",
                table: "screening");

            migrationBuilder.DropForeignKey(
                name: "FK_movie_screening_id",
                table: "screening");

            migrationBuilder.DropForeignKey(
                name: "FK_cinema_hall_seat_id",
                table: "seat");

            migrationBuilder.DropTable(
                name: "ticket");

            migrationBuilder.DropTable(
                name: "reservation_seat");

            migrationBuilder.DropTable(
                name: "reservation");

            migrationBuilder.AddForeignKey(
                name: "FK_cinema_hall_cinema_building_id",
                table: "cinema_hall",
                column: "cinema_building_id",
                principalTable: "cinema_building",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_screening_cinema_hall_id",
                table: "screening",
                column: "cinema_hall_id",
                principalTable: "cinema_hall",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_screening_movie_id",
                table: "screening",
                column: "movie_id",
                principalTable: "movie",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_seat_cinema_hall_id",
                table: "seat",
                column: "cinema_hall_id",
                principalTable: "cinema_hall",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
