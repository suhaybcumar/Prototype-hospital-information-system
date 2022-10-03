﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PHIMS.Migrations
{
    public partial class gghk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "Pateint",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "Pateint");
        }
    }
}
