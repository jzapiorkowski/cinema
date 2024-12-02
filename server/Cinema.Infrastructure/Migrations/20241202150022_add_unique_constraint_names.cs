using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_unique_constraint_names : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_seat_cinema_hall_id_row_column",
                table: "seat",
                newName: "IX_UQ_Seat_CinemaHall_Row_Column");

            migrationBuilder.RenameIndex(
                name: "IX_cinema_hall_cinema_building_id_number",
                table: "cinema_hall",
                newName: "IX_UQ_CinemaHall_CinemaBuildingId_Number");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_UQ_Seat_CinemaHall_Row_Column",
                table: "seat",
                newName: "IX_seat_cinema_hall_id_row_column");

            migrationBuilder.RenameIndex(
                name: "IX_UQ_CinemaHall_CinemaBuildingId_Number",
                table: "cinema_hall",
                newName: "IX_cinema_hall_cinema_building_id_number");
        }
    }
}
