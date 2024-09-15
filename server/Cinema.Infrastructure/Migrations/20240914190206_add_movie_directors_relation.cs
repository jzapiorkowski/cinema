using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_movie_directors_relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                UPDATE movie
                SET director_id = (
                    SELECT p.id
                    FROM person p
                    WHERE p.first_name = SPLIT_PART(movie.director, ' ', 1)
                    AND p.last_name = SPLIT_PART(movie.director, ' ', 2)
                    LIMIT 1
                )
            ");
            
            migrationBuilder.DropColumn(
                name: "director",
                table: "movie");

            migrationBuilder.CreateIndex(
                name: "IX_movie_director_id",
                table: "movie",
                column: "director_id");

            migrationBuilder.AddForeignKey(
                name: "FK_movie_person_director_id",
                table: "movie",
                column: "director_id",
                principalTable: "person",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_movie_person_director_id",
                table: "movie");
            
            migrationBuilder.AddForeignKey(
                name: "FK_movie_person_Director_id",
                table: "movie",
                column: "DirectorId",
                principalTable: "person",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
