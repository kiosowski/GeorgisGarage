using Microsoft.EntityFrameworkCore.Migrations;

namespace GeorgisGarage.Data.Migrations
{
    public partial class UpdateModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoverPhotoRoads_Services_Id",
                table: "" +
                       "CoverPhoto" +
                       "Roads");

            migrationBuilder.DropForeignKey(
                name: "FK_CoverPhotoRoads_Images_ImageId",
                table: "CoverPhotoRoads");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CoverPhotoRoads",
                table: "CoverPhotoRoads");

            migrationBuilder.RenameTable(
                name: "CoverPhotoRoads",
                newName: "CoverPhotoService");

            migrationBuilder.RenameIndex(
                name: "IX_CoverPhotoRoads_ImageId",
                table: "CoverPhotoService",
                newName: "IX_CoverPhotoService_ImageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoverPhotoService",
                table: "CoverPhotoService",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CoverPhotoService_Services_Id",
                table: "CoverPhotoService",
                column: "Id",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CoverPhotoService_Images_ImageId",
                table: "CoverPhotoService",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoverPhotoService_Services_Id",
                table: "CoverPhotoService");

            migrationBuilder.DropForeignKey(
                name: "FK_CoverPhotoService_Images_ImageId",
                table: "CoverPhotoService");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CoverPhotoService",
                table: "CoverPhotoService");

            migrationBuilder.RenameTable(
                name: "CoverPhotoService",
                newName: "CoverPhotoRoads");

            migrationBuilder.RenameIndex(
                name: "IX_CoverPhotoService_ImageId",
                table: "CoverPhotoRoads",
                newName: "IX_CoverPhotoRoads_ImageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoverPhotoRoads",
                table: "CoverPhotoRoads",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CoverPhotoRoads_Services_Id",
                table: "CoverPhotoRoads",
                column: "Id",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CoverPhotoRoads_Images_ImageId",
                table: "CoverPhotoRoads",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
