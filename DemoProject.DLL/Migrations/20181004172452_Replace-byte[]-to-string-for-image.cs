using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DemoProject.DLL.Migrations
{
    public partial class Replacebytetostringforimage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "ShopItems");

            migrationBuilder.DropColumn(
                name: "Icon",
                table: "MenuItems");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "ShopItems",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IconPath",
                table: "MenuItems",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "ShopItems");

            migrationBuilder.DropColumn(
                name: "IconPath",
                table: "MenuItems");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "ShopItems",
                type: "image",
                nullable: false,
                defaultValue: new byte[] {  });

            migrationBuilder.AddColumn<byte[]>(
                name: "Icon",
                table: "MenuItems",
                type: "image",
                nullable: false,
                defaultValue: new byte[] {  });
        }
    }
}
