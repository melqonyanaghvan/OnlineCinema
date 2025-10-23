using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineCinema.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDurationMinutes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DurationInMinutes",
                table: "Movies",
                newName: "DurationMinutes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DurationMinutes",
                table: "Movies",
                newName: "DurationInMinutes");
        }
    }
}
