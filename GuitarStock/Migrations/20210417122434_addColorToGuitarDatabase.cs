using Microsoft.EntityFrameworkCore.Migrations;

namespace GuitarStock.Migrations
{
    public partial class addColorToGuitarDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Guitars",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Guitars");
        }
    }
}
