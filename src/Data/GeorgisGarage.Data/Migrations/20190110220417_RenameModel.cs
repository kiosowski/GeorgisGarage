using Microsoft.EntityFrameworkCore.Migrations;

namespace GeorgisGarage.Data.Migrations
{
    public partial class RenameModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Services_RoadId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "RoadId",
                table: "Comments",
                newName: "ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_RoadId",
                table: "Comments",
                newName: "IX_Comments_ServiceId");

            migrationBuilder.AddColumn<double>(
                name: "AveragePosterRating",
                table: "Services",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AverageRating",
                table: "Services",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "PleasureRating",
                table: "Services",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SurfaceRating",
                table: "Services",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ViewRating",
                table: "Services",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Services_ServiceId",
                table: "Comments",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Services_ServiceId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "AveragePosterRating",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "AverageRating",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "PleasureRating",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "SurfaceRating",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "ViewRating",
                table: "Services");

            migrationBuilder.RenameColumn(
                name: "ServiceId",
                table: "Comments",
                newName: "RoadId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_ServiceId",
                table: "Comments",
                newName: "IX_Comments_RoadId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Services_RoadId",
                table: "Comments",
                column: "RoadId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
