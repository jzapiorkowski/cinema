using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Cinema.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class create_cinema_building : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cinema_building",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    address = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cinema_building", x => x.Id);
                });
            
            migrationBuilder.AddColumn<int>(
                name: "CinemaBuildingId",
                table: "cinema_hall",
                type: "integer",
                nullable: false,
                defaultValue: 0);
            
            migrationBuilder.CreateIndex(
                name: "IX_cinema_hall_CinemaBuildingId",
                table: "cinema_hall",
                column: "CinemaBuildingId");

            migrationBuilder.AddForeignKey(
                name: "FK_cinema_hall_cinema_building_CinemaBuildingId",
                table: "cinema_hall",
                column: "CinemaBuildingId",
                principalTable: "cinema_building",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cinema_hall_cinema_building_CinemaBuildingId",
                table: "cinema_hall");

            migrationBuilder.DropTable(
                name: "cinema_building");

            migrationBuilder.DropIndex(
                name: "IX_cinema_hall_CinemaBuildingId",
                table: "cinema_hall");

            migrationBuilder.DropColumn(
                name: "CinemaBuildingId",
                table: "cinema_hall");
        }
    }
}
