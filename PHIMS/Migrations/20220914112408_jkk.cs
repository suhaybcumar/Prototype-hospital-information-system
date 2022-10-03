using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PHIMS.Migrations
{
    public partial class jkk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctor_AspNetUsers_UserId",
                table: "Doctor");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Doctor",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Doctor_UserId",
                table: "Doctor",
                newName: "IX_Doctor_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctor_AspNetUsers_Id",
                table: "Doctor",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctor_AspNetUsers_Id",
                table: "Doctor");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Doctor",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Doctor_Id",
                table: "Doctor",
                newName: "IX_Doctor_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctor_AspNetUsers_UserId",
                table: "Doctor",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
