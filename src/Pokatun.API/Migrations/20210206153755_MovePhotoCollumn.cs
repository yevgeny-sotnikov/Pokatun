using Microsoft.EntityFrameworkCore.Migrations;

namespace Pokatun.API.Migrations
{
    public partial class MovePhotoCollumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Hotels_PhotoUrl",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "Hotels");

            migrationBuilder.AddColumn<string>(
                name: "PhotoName",
                table: "Accounts",
                maxLength: 256,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_PhotoName",
                table: "Accounts",
                column: "PhotoName",
                unique: true,
                filter: "[PhotoName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Accounts_PhotoName",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "PhotoName",
                table: "Accounts");

            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "Hotels",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_PhotoUrl",
                table: "Hotels",
                column: "PhotoUrl",
                unique: true,
                filter: "[PhotoUrl] IS NOT NULL");
        }
    }
}
