using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pokatun.API.Migrations
{
    public partial class ResetTokenAddition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResetToken",
                table: "Hotels",
                maxLength: 8,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ResetTokenExpires",
                table: "Hotels",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_ResetToken",
                table: "Hotels",
                column: "ResetToken",
                unique: true,
                filter: "[ResetToken] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Hotels_ResetToken",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "ResetToken",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "ResetTokenExpires",
                table: "Hotels");
        }
    }
}
