using Microsoft.EntityFrameworkCore.Migrations;

namespace Pokatun.API.Migrations
{
    public partial class NewHotelLocationFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Hotels",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Hotels",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Longtitude",
                table: "Hotels",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "Longtitude",
                table: "Hotels");
        }
    }
}
