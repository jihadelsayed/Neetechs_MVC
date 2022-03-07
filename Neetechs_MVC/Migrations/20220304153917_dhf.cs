using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Neetechs_MVC.Migrations
{
    public partial class dhf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Products_MovieId",
                table: "OrderItem");

            migrationBuilder.DropIndex(
                name: "IX_OrderItem_MovieId",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "File",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "OrderItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_ProductId",
                table: "OrderItem",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Products_ProductId",
                table: "OrderItem",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Products_ProductId",
                table: "OrderItem");

            migrationBuilder.DropIndex(
                name: "IX_OrderItem_ProductId",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "OrderItem");

            migrationBuilder.AddColumn<byte[]>(
                name: "File",
                table: "AspNetUsers",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_MovieId",
                table: "OrderItem",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Products_MovieId",
                table: "OrderItem",
                column: "MovieId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
