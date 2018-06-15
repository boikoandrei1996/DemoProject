using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DemoProject.DLL.Migrations
{
    public partial class smallrefactoring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfRejected",
                table: "Orders",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_Title",
                table: "Discounts",
                column: "Title",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Discounts_Title",
                table: "Discounts");

            migrationBuilder.DropColumn(
                name: "DateOfRejected",
                table: "Orders");
        }
    }
}
