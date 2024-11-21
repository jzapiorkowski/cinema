using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fix_ondelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cinema_hall_cinema_building_cinema_building_id",
                table: "cinema_hall");

            migrationBuilder.DropForeignKey(
                name: "FK_screening_cinema_hall_cinema_hall_id",
                table: "screening");

            migrationBuilder.DropForeignKey(
                name: "FK_seat_cinema_hall_cinema_hall_id",
                table: "seat");

            migrationBuilder.AddForeignKey(
                name: "FK_cinema_hall_cinema_building_cinema_building_id",
                table: "cinema_hall",
                column: "cinema_building_id",
                principalTable: "cinema_building",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_screening_cinema_hall_cinema_hall_id",
                table: "screening",
                column: "cinema_hall_id",
                principalTable: "cinema_hall",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_seat_cinema_hall_cinema_hall_id",
                table: "seat",
                column: "cinema_hall_id",
                principalTable: "cinema_hall",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cinema_hall_cinema_building_cinema_building_id",
                table: "cinema_hall");

            migrationBuilder.DropForeignKey(
                name: "FK_screening_cinema_hall_cinema_hall_id",
                table: "screening");

            migrationBuilder.DropForeignKey(
                name: "FK_seat_cinema_hall_cinema_hall_id",
                table: "seat");

            migrationBuilder.AddForeignKey(
                name: "FK_cinema_hall_cinema_building_cinema_building_id",
                table: "cinema_hall",
                column: "cinema_building_id",
                principalTable: "cinema_building",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_screening_cinema_hall_cinema_hall_id",
                table: "screening",
                column: "cinema_hall_id",
                principalTable: "cinema_hall",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_seat_cinema_hall_cinema_hall_id",
                table: "seat",
                column: "cinema_hall_id",
                principalTable: "cinema_hall",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
