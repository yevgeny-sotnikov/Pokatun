using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pokatun.API.Migrations
{
    public partial class MoveFieldsFromHotelEntityTOAccountEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Hotels_Email",
                table: "Hotels");

            migrationBuilder.DropIndex(
                name: "IX_Hotels_ResetToken",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "ResetToken",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "ResetTokenExpires",
                table: "Hotels");

            migrationBuilder.AddColumn<long>(
                name: "AccountId",
                table: "Hotels",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "ResetToken",
                table: "Accounts",
                maxLength: 8,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ResetTokenExpires",
                table: "Accounts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Accounts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_AccountId",
                table: "Hotels",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_ResetToken",
                table: "Accounts",
                column: "ResetToken",
                unique: true,
                filter: "[ResetToken] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Hotels_Accounts_AccountId",
                table: "Hotels",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_Accounts_AccountId",
                table: "Hotels");

            migrationBuilder.DropIndex(
                name: "IX_Hotels_AccountId",
                table: "Hotels");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_ResetToken",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "ResetToken",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "ResetTokenExpires",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Accounts");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Hotels",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "Hotels",
                type: "varbinary(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: new byte[] {  });

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "Hotels",
                type: "varbinary(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: new byte[] {  });

            migrationBuilder.AddColumn<string>(
                name: "ResetToken",
                table: "Hotels",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ResetTokenExpires",
                table: "Hotels",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_Email",
                table: "Hotels",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_ResetToken",
                table: "Hotels",
                column: "ResetToken",
                unique: true,
                filter: "[ResetToken] IS NOT NULL");
        }
    }
}
