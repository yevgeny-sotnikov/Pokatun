using Microsoft.EntityFrameworkCore.Migrations;

namespace Pokatun.API.Migrations
{
    public partial class HotelsPhonesOneToManyRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Hotels");

            migrationBuilder.CreateTable(
                name: "Phone",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(maxLength: 16, nullable: false),
                    HotelId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phone", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Phone_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Phone_HotelId",
                table: "Phone",
                column: "HotelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Phone");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Hotels",
                type: "nvarchar(16)",
                maxLength: 16,
                nullable: false,
                defaultValue: "");
        }
    }
}
