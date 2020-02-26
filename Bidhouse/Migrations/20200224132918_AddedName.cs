using Microsoft.EntityFrameworkCore.Migrations;

namespace Bidhouse.Migrations
{
    public partial class AddedName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Posts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Posts");
        }
    }
}
