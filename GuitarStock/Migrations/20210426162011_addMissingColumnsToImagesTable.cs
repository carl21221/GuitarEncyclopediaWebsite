using Microsoft.EntityFrameworkCore.Migrations;

namespace GuitarStock.Migrations
{
    public partial class addMissingColumnsToImagesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GuitarID",
                table: "Images",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "URL",
                table: "Images",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GuitarID",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "URL",
                table: "Images");
        }
    }
}
