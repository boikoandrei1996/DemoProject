using Microsoft.EntityFrameworkCore.Migrations;

namespace DemoProject.DLL.Migrations
{
    public partial class ChangeCartShopItemmodelindex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartShopItems_ShopItems_ShopItemId",
                table: "CartShopItems");

            migrationBuilder.RenameColumn(
                name: "ShopItemId",
                table: "CartShopItems",
                newName: "ShopItemDetailId");

            migrationBuilder.RenameIndex(
                name: "IX_CartShopItems_ShopItemId",
                table: "CartShopItems",
                newName: "IX_CartShopItems_ShopItemDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartShopItems_ShopItemDetails_ShopItemDetailId",
                table: "CartShopItems",
                column: "ShopItemDetailId",
                principalTable: "ShopItemDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartShopItems_ShopItemDetails_ShopItemDetailId",
                table: "CartShopItems");

            migrationBuilder.RenameColumn(
                name: "ShopItemDetailId",
                table: "CartShopItems",
                newName: "ShopItemId");

            migrationBuilder.RenameIndex(
                name: "IX_CartShopItems_ShopItemDetailId",
                table: "CartShopItems",
                newName: "IX_CartShopItems_ShopItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartShopItems_ShopItems_ShopItemId",
                table: "CartShopItems",
                column: "ShopItemId",
                principalTable: "ShopItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
