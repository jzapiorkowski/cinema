using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_cinema_hall_screenings_relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_cinema_halls",
                table: "cinema_halls");

            migrationBuilder.RenameTable(
                name: "cinema_halls",
                newName: "cinema_hall");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "cinema_hall",
                newName: "id");

            migrationBuilder.AddColumn<int>(
                name: "cinema_hall_id",
                table: "screening",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_cinema_hall",
                table: "cinema_hall",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_screening_cinema_hall_id",
                table: "screening",
                column: "cinema_hall_id");

            migrationBuilder.AddForeignKey(
                name: "FK_screening_cinema_hall_cinema_hall_id",
                table: "screening",
                column: "cinema_hall_id",
                principalTable: "cinema_hall",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_screening_cinema_hall_cinema_hall_id",
                table: "screening");

            migrationBuilder.DropIndex(
                name: "IX_screening_cinema_hall_id",
                table: "screening");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cinema_hall",
                table: "cinema_hall");

            migrationBuilder.DropColumn(
                name: "cinema_hall_id",
                table: "screening");

            migrationBuilder.RenameTable(
                name: "cinema_hall",
                newName: "cinema_halls");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "cinema_halls",
                newName: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_cinema_halls",
                table: "cinema_halls",
                column: "Id");
        }
    }
}
