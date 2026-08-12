using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace PlaceService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LocationFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvgPressure",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "AvgTemperature",
                table: "Locations");

            migrationBuilder.AlterColumn<Point>(
                name: "Coordinates",
                table: "Locations",
                type: "geometry(Point,4326)",
                nullable: false,
                oldClrType: typeof(Point),
                oldType: "geometry");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Point>(
                name: "Coordinates",
                table: "Locations",
                type: "geometry",
                nullable: false,
                oldClrType: typeof(Point),
                oldType: "geometry(Point,4326)");

            migrationBuilder.AddColumn<double>(
                name: "AvgPressure",
                table: "Locations",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AvgTemperature",
                table: "Locations",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
