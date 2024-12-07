using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDev.API.Migrations
{
    /// <inheritdoc />
    public partial class testTwomigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Building",
                table: "Waypoints",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Floor",
                table: "Waypoints",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Building",
                table: "Waypoints");

            migrationBuilder.DropColumn(
                name: "Floor",
                table: "Waypoints");
        }
    }
}
