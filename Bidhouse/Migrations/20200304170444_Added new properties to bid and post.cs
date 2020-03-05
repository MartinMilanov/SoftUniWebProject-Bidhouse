using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bidhouse.Migrations
{
    public partial class Addednewpropertiestobidandpost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Posts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Bids",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Days",
                table: "Bids",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "Days",
                table: "Bids");
        }
    }
}
