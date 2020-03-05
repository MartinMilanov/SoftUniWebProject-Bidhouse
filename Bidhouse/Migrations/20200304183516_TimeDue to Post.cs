using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bidhouse.Migrations
{
    public partial class TimeDuetoPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "TimeDue",
                table: "Posts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeDue",
                table: "Posts");
        }
    }
}
