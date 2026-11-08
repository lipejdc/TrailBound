using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrailBound.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDistanceInMetersProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Route_DistanceInKm",
                table: "Activities",
                newName: "Route_DistanceInKilometers");

            migrationBuilder.AddColumn<int>(
                name: "Route_DistanceInMeters",
                table: "Activities",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Route_DistanceInMeters",
                table: "Activities");

            migrationBuilder.RenameColumn(
                name: "Route_DistanceInKilometers",
                table: "Activities",
                newName: "Route_DistanceInKm");
        }
    }
}
