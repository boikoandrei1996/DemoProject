using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DemoProject.DLL.Migrations
{
    public partial class updateimagecolumntype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Image",
                table: "ShopItems",
                type: "image",
                nullable: false,
                oldClrType: typeof(byte[]));

            migrationBuilder.AlterColumn<byte[]>(
                name: "Icon",
                table: "MenuItems",
                type: "image",
                nullable: false,
                oldClrType: typeof(byte[]));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Image",
                table: "ShopItems",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "image");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Icon",
                table: "MenuItems",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "image");
        }
    }
}
