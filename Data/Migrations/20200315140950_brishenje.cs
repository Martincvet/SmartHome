using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class brishenje : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShopItems_Buys_BuyId",
                table: "ShopItems");

            migrationBuilder.DropTable(
                name: "Buys");

            migrationBuilder.DropIndex(
                name: "IX_ShopItems_BuyId",
                table: "ShopItems");

            migrationBuilder.DropColumn(
                name: "BuyId",
                table: "ShopItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BuyId",
                table: "ShopItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Buys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Buys_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShopItems_BuyId",
                table: "ShopItems",
                column: "BuyId");

            migrationBuilder.CreateIndex(
                name: "IX_Buys_UserId",
                table: "Buys",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShopItems_Buys_BuyId",
                table: "ShopItems",
                column: "BuyId",
                principalTable: "Buys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
