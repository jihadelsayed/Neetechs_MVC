using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Neetechs_MVC.Migrations
{
    public partial class jfg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Category_SubCategoryId",
                table: "Category");

            migrationBuilder.RenameColumn(
                name: "SubCategoryId",
                table: "Category",
                newName: "FatherCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Category_SubCategoryId",
                table: "Category",
                newName: "IX_Category_FatherCategoryId");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Category",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<byte[]>(
                name: "File",
                table: "Category",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Category",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Category",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Category_FatherCategoryId",
                table: "Category",
                column: "FatherCategoryId",
                principalTable: "Category",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Category_FatherCategoryId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "File",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Category");

            migrationBuilder.RenameColumn(
                name: "FatherCategoryId",
                table: "Category",
                newName: "SubCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Category_FatherCategoryId",
                table: "Category",
                newName: "IX_Category_SubCategoryId");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Category",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Category_SubCategoryId",
                table: "Category",
                column: "SubCategoryId",
                principalTable: "Category",
                principalColumn: "Id");
        }
    }
}
