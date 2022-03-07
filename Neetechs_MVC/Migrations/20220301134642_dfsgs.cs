using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Neetechs_MVC.Migrations
{
    public partial class dfsgs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_AspNetUsers_UserId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_UserId",
                table: "Services");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Services",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfileId",
                table: "Services",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Services_ProfileId",
                table: "Services",
                column: "ProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_AspNetUsers_ProfileId",
                table: "Services",
                column: "ProfileId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_AspNetUsers_ProfileId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_ProfileId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "Services");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Services",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Services_UserId",
                table: "Services",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_AspNetUsers_UserId",
                table: "Services",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
