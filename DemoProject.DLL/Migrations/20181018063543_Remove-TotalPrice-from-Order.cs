using Microsoft.EntityFrameworkCore.Migrations;

namespace DemoProject.DLL.Migrations
{
    public partial class RemoveTotalPricefromOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Orders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "Orders",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
