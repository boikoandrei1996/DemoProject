using Microsoft.EntityFrameworkCore.Migrations;

namespace DemoProject.DAL.Migrations
{
    public partial class updatechangehistorymodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TableName",
                table: "History");

            migrationBuilder.AddColumn<short>(
                name: "Action",
                table: "History",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "Table",
                table: "History",
                nullable: false,
                defaultValue: (short)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Action",
                table: "History");

            migrationBuilder.DropColumn(
                name: "Table",
                table: "History");

            migrationBuilder.AddColumn<string>(
                name: "TableName",
                table: "History",
                nullable: false,
                defaultValue: "");
        }
    }
}
