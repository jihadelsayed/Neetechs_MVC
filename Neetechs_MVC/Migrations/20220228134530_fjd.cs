using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Neetechs_MVC.Migrations
{
    public partial class fjd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Products",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProfileId",
                table: "Products",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProfileId",
                table: "Products",
                column: "ProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_ProfileId",
                table: "Products",
                column: "ProfileId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_ProfileId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProfileId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "Products",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
