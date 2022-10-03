using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PHIMS.Migrations
{
    public partial class kk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Pateint",
                type: "text",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AddColumn<string>(
                name: "Fee",
                table: "Pateint",
                type: "text",
                nullable: false,
                defaultValue: "");

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
                name: "Fee",
                table: "Pateint");

            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "Pateint");

            migrationBuilder.AlterColumn<bool>(
                name: "Type",
                table: "Pateint",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
