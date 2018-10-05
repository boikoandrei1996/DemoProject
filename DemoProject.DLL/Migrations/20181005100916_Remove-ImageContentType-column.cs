using Microsoft.EntityFrameworkCore.Migrations;

namespace DemoProject.DLL.Migrations
{
    public partial class RemoveImageContentTypecolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageContentType",
                table: "ShopItems");

            migrationBuilder.DropColumn(
                name: "IconContentType",
                table: "MenuItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageContentType",
                table: "ShopItems",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IconContentType",
                table: "MenuItems",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }
    }
}
