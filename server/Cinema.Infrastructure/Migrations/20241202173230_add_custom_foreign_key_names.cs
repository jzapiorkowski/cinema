using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_custom_foreign_key_names : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cinema_hall_cinema_building_cinema_building_id",
                table: "cinema_hall");

            migrationBuilder.DropForeignKey(
                name: "FK_movie_person_director_id",
                table: "movie");

            migrationBuilder.DropForeignKey(
                name: "FK_movie_actor_movie_movie_id",
                table: "movie_actor");

            migrationBuilder.DropForeignKey(
                name: "FK_movie_actor_person_actor_id",
                table: "movie_actor");

            migrationBuilder.DropForeignKey(
                name: "FK_screening_cinema_hall_cinema_hall_id",
                table: "screening");

            migrationBuilder.DropForeignKey(
                name: "FK_screening_movie_movie_id",
                table: "screening");

            migrationBuilder.DropForeignKey(
                name: "FK_seat_cinema_hall_cinema_hall_id",
                table: "seat");

            migrationBuilder.RenameIndex(
                name: "IX_UQ_Seat_CinemaHall_Row_Column",
                table: "seat",
                newName: "IX_UQ_seat_cinema_hall_row_column");

            migrationBuilder.RenameIndex(
                name: "IX_UQ_CinemaHall_CinemaBuildingId_Number",
                table: "cinema_hall",
                newName: "IX_UQ_cinema_hall_cinema_building_id_number");

            migrationBuilder.AddForeignKey(
                name: "FK_cinema_hall_cinema_building_id",
                table: "cinema_hall",
                column: "cinema_building_id",
                principalTable: "cinema_building",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_movie_director_id",
                table: "movie",
                column: "director_id",
                principalTable: "person",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_movie_actor_actor_id",
                table: "movie_actor",
                column: "actor_id",
                principalTable: "person",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_movie_actor_movie_id",
                table: "movie_actor",
                column: "movie_id",
                principalTable: "movie",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cinema_hall_cinema_building_id",
                table: "cinema_hall");

            migrationBuilder.DropForeignKey(
                name: "FK_movie_director_id",
                table: "movie");

            migrationBuilder.DropForeignKey(
                name: "FK_movie_actor_actor_id",
                table: "movie_actor");

            migrationBuilder.DropForeignKey(
                name: "FK_movie_actor_movie_id",
                table: "movie_actor");

            migrationBuilder.DropForeignKey(
                name: "FK_screening_cinema_hall_id",
                table: "screening");

            migrationBuilder.DropForeignKey(
                name: "FK_screening_movie_id",
                table: "screening");

            migrationBuilder.DropForeignKey(
                name: "FK_seat_cinema_hall_id",
                table: "seat");

            migrationBuilder.RenameIndex(
                name: "IX_UQ_seat_cinema_hall_row_column",
                table: "seat",
                newName: "IX_UQ_Seat_CinemaHall_Row_Column");

            migrationBuilder.RenameIndex(
                name: "IX_UQ_cinema_hall_cinema_building_id_number",
                table: "cinema_hall",
                newName: "IX_UQ_CinemaHall_CinemaBuildingId_Number");

            migrationBuilder.AddForeignKey(
                name: "FK_cinema_hall_cinema_building_cinema_building_id",
                table: "cinema_hall",
                column: "cinema_building_id",
                principalTable: "cinema_building",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_movie_person_director_id",
                table: "movie",
                column: "director_id",
                principalTable: "person",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_movie_actor_movie_movie_id",
                table: "movie_actor",
                column: "movie_id",
                principalTable: "movie",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_movie_actor_person_actor_id",
                table: "movie_actor",
                column: "actor_id",
                principalTable: "person",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_screening_cinema_hall_cinema_hall_id",
                table: "screening",
                column: "cinema_hall_id",
                principalTable: "cinema_hall",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_screening_movie_movie_id",
                table: "screening",
                column: "movie_id",
                principalTable: "movie",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_seat_cinema_hall_cinema_hall_id",
                table: "seat",
                column: "cinema_hall_id",
                principalTable: "cinema_hall",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
