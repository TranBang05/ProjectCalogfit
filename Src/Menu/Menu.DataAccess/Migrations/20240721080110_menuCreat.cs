﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Menu.DataAccess.Migrations
{
    public partial class menuCreat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Menus",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Menus");
        }
    }
}
