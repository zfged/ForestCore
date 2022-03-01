using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ForestCore.Migrations
{
    public partial class sgnoo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductPhotos_Product_ProductId",
                table: "ProductPhotos");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProductPhotos",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPhotos_Product_ProductId",
                table: "ProductPhotos",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductPhotos_Product_ProductId",
                table: "ProductPhotos");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProductPhotos",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPhotos_Product_ProductId",
                table: "ProductPhotos",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
