using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DemoProject.DLL.Migrations
{
    public partial class updateordercreationdatetime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfCreation",
                table: "Orders",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "GETDATE()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfCreation",
                table: "Orders",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "GETUTCDATE()");
        }
    }
}
