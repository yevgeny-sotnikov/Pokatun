using Microsoft.EntityFrameworkCore.Migrations;

namespace Pokatun.API.Migrations
{
    public partial class HotelNumbersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HotelNumbers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<short>(nullable: false),
                    Level = table.Column<int>(nullable: false),
                    RoomsAmount = table.Column<byte>(nullable: false),
                    VisitorsAmount = table.Column<byte>(nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: false),
                    CleaningNeeded = table.Column<bool>(nullable: false),
                    NutritionNeeded = table.Column<bool>(nullable: false),
                    BreakfastIncluded = table.Column<bool>(nullable: false),
                    DinnerIncluded = table.Column<bool>(nullable: false),
                    SupperIncluded = table.Column<bool>(nullable: false),
                    Price = table.Column<long>(nullable: false),
                    HotelId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelNumbers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HotelNumbers_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HotelNumbers_HotelId",
                table: "HotelNumbers",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_HotelNumbers_Number",
                table: "HotelNumbers",
                column: "Number",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HotelNumbers");
        }
    }
}
