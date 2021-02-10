using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pokatun.API.Migrations
{
    public partial class BidsTableSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bids",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelNumberId = table.Column<long>(nullable: false),
                    Price = table.Column<long>(nullable: false),
                    Discount = table.Column<byte>(nullable: false),
                    MinDate = table.Column<DateTime>(nullable: false),
                    MaxDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bids", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bids_HotelNumbers_HotelNumberId",
                        column: x => x.HotelNumberId,
                        principalTable: "HotelNumbers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bids_HotelNumberId",
                table: "Bids",
                column: "HotelNumberId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bids");
        }
    }
}
