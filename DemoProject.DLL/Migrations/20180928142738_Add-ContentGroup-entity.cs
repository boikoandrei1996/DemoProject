using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DemoProject.DLL.Migrations
{
    public partial class AddContentGroupentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InfoObjects_Discounts_DiscountId",
                table: "InfoObjects");

            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.RenameColumn(
                name: "DiscountId",
                table: "InfoObjects",
                newName: "ContentGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_InfoObjects_DiscountId",
                table: "InfoObjects",
                newName: "IX_InfoObjects_ContentGroupId");

            migrationBuilder.CreateTable(
                name: "ContentGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(maxLength: 100, nullable: true),
                    Order = table.Column<int>(nullable: false),
                    GroupName = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentGroups", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_InfoObjects_ContentGroups_ContentGroupId",
                table: "InfoObjects",
                column: "ContentGroupId",
                principalTable: "ContentGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InfoObjects_ContentGroups_ContentGroupId",
                table: "InfoObjects");

            migrationBuilder.DropTable(
                name: "ContentGroups");

            migrationBuilder.RenameColumn(
                name: "ContentGroupId",
                table: "InfoObjects",
                newName: "DiscountId");

            migrationBuilder.RenameIndex(
                name: "IX_InfoObjects_ContentGroupId",
                table: "InfoObjects",
                newName: "IX_InfoObjects_DiscountId");

            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_Title",
                table: "Discounts",
                column: "Title",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_InfoObjects_Discounts_DiscountId",
                table: "InfoObjects",
                column: "DiscountId",
                principalTable: "Discounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
