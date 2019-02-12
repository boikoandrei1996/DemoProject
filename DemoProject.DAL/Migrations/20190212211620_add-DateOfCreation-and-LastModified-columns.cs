using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DemoProject.DAL.Migrations
{
    public partial class addDateOfCreationandLastModifiedcolumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfCreation",
                table: "Users",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfCreation",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Users");
        }
    }
}
