using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ForestCore.Migrations
{
    public partial class afbsdkb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductParametrs_Product_ProductId",
                table: "ProductParametrs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductParametrs",
                table: "ProductParametrs");

            migrationBuilder.RenameTable(
                name: "ProductParametrs",
                newName: "ProductParams");

            migrationBuilder.RenameIndex(
                name: "IX_ProductParametrs_ProductId",
                table: "ProductParams",
                newName: "IX_ProductParams_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductParams",
                table: "ProductParams",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductParams_Product_ProductId",
                table: "ProductParams",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductParams_Product_ProductId",
                table: "ProductParams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductParams",
                table: "ProductParams");

            migrationBuilder.RenameTable(
                name: "ProductParams",
                newName: "ProductParametrs");

            migrationBuilder.RenameIndex(
                name: "IX_ProductParams_ProductId",
                table: "ProductParametrs",
                newName: "IX_ProductParametrs_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductParametrs",
                table: "ProductParametrs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductParametrs_Product_ProductId",
                table: "ProductParametrs",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
