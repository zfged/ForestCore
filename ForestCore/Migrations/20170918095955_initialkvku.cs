using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ForestCore.Migrations
{
    public partial class initialkvku : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PathPhoto",
                table: "Blogs");

            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Blogs",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Blogs");

            migrationBuilder.AddColumn<byte[]>(
                name: "PathPhoto",
                table: "Blogs",
                nullable: true);
        }
    }
}
