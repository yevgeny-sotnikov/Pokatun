using Microsoft.EntityFrameworkCore.Migrations;

namespace Pokatun.API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hotel",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelName = table.Column<string>(maxLength: 64, nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 16, nullable: false),
                    Email = table.Column<string>(maxLength: 64, nullable: false),
                    Password = table.Column<string>(maxLength: 32, nullable: false),
                    FullCompanyName = table.Column<string>(maxLength: 128, nullable: false),
                    BankCard = table.Column<long>(nullable: true),
                    IBAN = table.Column<string>(maxLength: 34, nullable: true),
                    BankName = table.Column<string>(maxLength: 32, nullable: false),
                    USREOU = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotel", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hotel_Email",
                table: "Hotel",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Hotel");
        }
    }
}
