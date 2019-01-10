using Microsoft.EntityFrameworkCore.Migrations;

namespace GeorgisGarage.Data.Migrations
{
    public partial class IdAddToProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageId",
                table: "Products",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Products");
        }
    }
}
