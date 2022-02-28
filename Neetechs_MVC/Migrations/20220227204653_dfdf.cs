using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Neetechs_MVC.Migrations
{
    public partial class dfdf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Read",
                table: "Notifications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Read",
                table: "Messages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Read",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Read",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Messages");
        }
    }
}
