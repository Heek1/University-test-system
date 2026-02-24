using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace University_test_system.Migrations
{
    /// <inheritdoc />
    public partial class AddTimeSpentToAttempt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TimeSpentSeconds",
                table: "Attempts",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
