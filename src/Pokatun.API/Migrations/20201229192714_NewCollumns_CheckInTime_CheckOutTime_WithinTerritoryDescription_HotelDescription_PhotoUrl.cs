using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pokatun.API.Migrations
{
    public partial class NewCollumns_CheckInTime_CheckOutTime_WithinTerritoryDescription_HotelDescription_PhotoUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "CheckInTime",
                table: "Hotels",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "CheckOutTime",
                table: "Hotels",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HotelDescription",
                table: "Hotels",
                maxLength: 600,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "Hotels",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WithinTerritoryDescription",
                table: "Hotels",
                maxLength: 200,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_PhotoUrl",
                table: "Hotels",
                column: "PhotoUrl",
                unique: true,
                filter: "[PhotoUrl] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Hotels_PhotoUrl",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "CheckInTime",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "CheckOutTime",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "HotelDescription",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "WithinTerritoryDescription",
                table: "Hotels");
        }
    }
}
