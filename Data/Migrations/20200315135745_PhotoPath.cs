using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class PhotoPath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "ShopItems");

            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                table: "ShopItems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoPath",
                table: "ShopItems");

            migrationBuilder.AddColumn<byte[]>(
                name: "Photo",
                table: "ShopItems",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
