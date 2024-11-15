using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class no_duplicate_seat_for_cinema_hall : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_seat_cinema_hall_id",
                table: "seat");

            migrationBuilder.CreateIndex(
                name: "IX_seat_cinema_hall_id_row_column",
                table: "seat",
                columns: new[] { "cinema_hall_id", "row", "column" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_seat_cinema_hall_id_row_column",
                table: "seat");

            migrationBuilder.CreateIndex(
                name: "IX_seat_cinema_hall_id",
                table: "seat",
                column: "cinema_hall_id");
        }
    }
}
