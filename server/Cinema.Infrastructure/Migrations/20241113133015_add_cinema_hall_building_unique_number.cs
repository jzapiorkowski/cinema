using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_cinema_hall_building_unique_number : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_cinema_hall_CinemaBuildingId",
                table: "cinema_hall");

            migrationBuilder.AddColumn<int>(
                name: "number",
                table: "cinema_hall",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_cinema_hall_CinemaBuildingId_number",
                table: "cinema_hall",
                columns: new[] { "CinemaBuildingId", "number" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_cinema_hall_CinemaBuildingId_number",
                table: "cinema_hall");

            migrationBuilder.DropColumn(
                name: "number",
                table: "cinema_hall");

            migrationBuilder.CreateIndex(
                name: "IX_cinema_hall_CinemaBuildingId",
                table: "cinema_hall",
                column: "CinemaBuildingId");
        }
    }
}
