using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pokatun.API.Migrations
{
    public partial class CreatePasswordHashAndPasswordSaltFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Hotels");

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "Hotels",
                maxLength: 64,
                nullable: false,
                defaultValue: new byte[] {  });

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "Hotels",
                maxLength: 128,
                nullable: false,
                defaultValue: new byte[] {  });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Hotels");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Hotels",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "");
        }
    }
}
