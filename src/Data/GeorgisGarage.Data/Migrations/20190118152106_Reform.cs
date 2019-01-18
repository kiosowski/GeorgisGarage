using Microsoft.EntityFrameworkCore.Migrations;

namespace GeorgisGarage.Data.Migrations
{
    public partial class Reform : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ViewRating",
                table: "Services",
                newName: "TimeRating");

            migrationBuilder.RenameColumn(
                name: "SurfaceRating",
                table: "Services",
                newName: "PerformanceRating");

            migrationBuilder.RenameColumn(
                name: "PleasureRating",
                table: "Services",
                newName: "MaterialRating");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TimeRating",
                table: "Services",
                newName: "ViewRating");

            migrationBuilder.RenameColumn(
                name: "PerformanceRating",
                table: "Services",
                newName: "SurfaceRating");

            migrationBuilder.RenameColumn(
                name: "MaterialRating",
                table: "Services",
                newName: "PleasureRating");
        }
    }
}
