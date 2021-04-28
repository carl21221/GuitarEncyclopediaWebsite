using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GuitarStock.Migrations
{
    public partial class addGuitarsToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Guitars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FretboardMaterial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BodyMaterial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NeckJoint = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StringCount = table.Column<int>(type: "int", nullable: false),
                    FretCount = table.Column<int>(type: "int", nullable: false),
                    FretSize = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InlayStyle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Binding = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PickupConfiguration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BridgePickup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MiddlePickup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NeckPickup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PickupSwitch = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActivePickups = table.Column<bool>(type: "bit", nullable: false),
                    BridgeType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guitars", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Guitars");
        }
    }
}
