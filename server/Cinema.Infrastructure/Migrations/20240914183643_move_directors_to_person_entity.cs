using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class move_directors_to_person_entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "director_id",
                table: "movie",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            // Step 2: Insert distinct directors into the person table
            migrationBuilder.Sql(@"
                INSERT INTO person (first_name, last_name, birth_date)
                SELECT DISTINCT
                    SPLIT_PART(director, ' ', 1) AS FirstName,
                    SPLIT_PART(director, ' ', 2) AS LastName,
                    CURRENT_DATE AS BirthDate
                FROM movie
                WHERE director IS NOT NULL
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "director_id",
                table: "movie");
        }
    }
}
