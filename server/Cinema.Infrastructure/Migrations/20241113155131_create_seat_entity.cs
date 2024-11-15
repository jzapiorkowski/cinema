using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Cinema.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class create_seat_entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cinema_hall_cinema_building_CinemaBuildingId",
                table: "cinema_hall");

            migrationBuilder.RenameColumn(
                name: "CinemaBuildingId",
                table: "cinema_hall",
                newName: "cinema_building_id");

            migrationBuilder.RenameIndex(
                name: "IX_cinema_hall_CinemaBuildingId_number",
                table: "cinema_hall",
                newName: "IX_cinema_hall_cinema_building_id_number");

            migrationBuilder.AddColumn<int>(
                name: "capacity",
                table: "cinema_hall",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "seat",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    row = table.Column<int>(type: "integer", nullable: false),
                    column = table.Column<int>(type: "integer", nullable: false),
                    type = table.Column<string>(type: "text", nullable: false),
                    cinema_hall_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_seat", x => x.id);
                    table.ForeignKey(
                        name: "FK_seat_cinema_hall_cinema_hall_id",
                        column: x => x.cinema_hall_id,
                        principalTable: "cinema_hall",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_seat_cinema_hall_id",
                table: "seat",
                column: "cinema_hall_id");

            migrationBuilder.AddForeignKey(
                name: "FK_cinema_hall_cinema_building_cinema_building_id",
                table: "cinema_hall",
                column: "cinema_building_id",
                principalTable: "cinema_building",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cinema_hall_cinema_building_cinema_building_id",
                table: "cinema_hall");

            migrationBuilder.DropTable(
                name: "seat");

            migrationBuilder.DropColumn(
                name: "capacity",
                table: "cinema_hall");

            migrationBuilder.RenameColumn(
                name: "cinema_building_id",
                table: "cinema_hall",
                newName: "CinemaBuildingId");

            migrationBuilder.RenameIndex(
                name: "IX_cinema_hall_cinema_building_id_number",
                table: "cinema_hall",
                newName: "IX_cinema_hall_CinemaBuildingId_number");

            migrationBuilder.AddForeignKey(
                name: "FK_cinema_hall_cinema_building_CinemaBuildingId",
                table: "cinema_hall",
                column: "CinemaBuildingId",
                principalTable: "cinema_building",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
