using Microsoft.EntityFrameworkCore.Migrations;

namespace Pokatun.API.Migrations
{
    public partial class NewUniqueConstraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Hotels_IBAN",
                table: "Hotels",
                column: "IBAN",
                unique: true,
                filter: "[IBAN] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_USREOU",
                table: "Hotels",
                column: "USREOU",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Hotels_IBAN",
                table: "Hotels");

            migrationBuilder.DropIndex(
                name: "IX_Hotels_USREOU",
                table: "Hotels");
        }
    }
}
