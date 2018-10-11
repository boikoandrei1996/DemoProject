using Microsoft.EntityFrameworkCore.Migrations;

namespace DemoProject.DLL.Migrations
{
    public partial class AddSubOrdercolumntoShopItemDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubOrder",
                table: "ShopItemDetails",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubOrder",
                table: "ShopItemDetails");
        }
    }
}
