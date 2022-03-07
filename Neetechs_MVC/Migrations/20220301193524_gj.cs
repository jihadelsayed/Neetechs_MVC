using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Neetechs_MVC.Migrations
{
    public partial class gj : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfileId",
                table: "Posts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfileId",
                table: "Notifications",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "ProfileId",
                table: "Messages",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_ProfileId",
                table: "Posts",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_ProfileId",
                table: "Notifications",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ProfileId",
                table: "Messages",
                column: "ProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_ProfileId",
                table: "Messages",
                column: "ProfileId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_AspNetUsers_ProfileId",
                table: "Notifications",
                column: "ProfileId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_ProfileId",
                table: "Posts",
                column: "ProfileId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_ProfileId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_AspNetUsers_ProfileId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_ProfileId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_ProfileId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_ProfileId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Messages_ProfileId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "Messages");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
