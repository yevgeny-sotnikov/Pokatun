using Microsoft.EntityFrameworkCore.Migrations;

namespace Pokatun.API.Migrations
{
    public partial class SocialResourcesOneToManyRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Phone_Hotels_HotelId",
                table: "Phone");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Phone",
                table: "Phone");

            migrationBuilder.RenameTable(
                name: "Phone",
                newName: "Phones");

            migrationBuilder.RenameIndex(
                name: "IX_Phone_HotelId",
                table: "Phones",
                newName: "IX_Phones_HotelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Phones",
                table: "Phones",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "SocialResources",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Link = table.Column<string>(maxLength: 64, nullable: false),
                    HotelId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialResources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SocialResources_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SocialResources_HotelId",
                table: "SocialResources",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_SocialResources_Link",
                table: "SocialResources",
                column: "Link",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Phones_Hotels_HotelId",
                table: "Phones",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Phones_Hotels_HotelId",
                table: "Phones");

            migrationBuilder.DropTable(
                name: "SocialResources");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Phones",
                table: "Phones");

            migrationBuilder.RenameTable(
                name: "Phones",
                newName: "Phone");

            migrationBuilder.RenameIndex(
                name: "IX_Phones_HotelId",
                table: "Phone",
                newName: "IX_Phone_HotelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Phone",
                table: "Phone",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Phone_Hotels_HotelId",
                table: "Phone",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
