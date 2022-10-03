using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PHIMS.Migrations
{
    public partial class ade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_AspNetUsers_ApplicationUserId",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "User",
                newName: "UserIdId");

            migrationBuilder.RenameIndex(
                name: "IX_User_ApplicationUserId",
                table: "User",
                newName: "IX_User_UserIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_AspNetUsers_UserIdId",
                table: "User",
                column: "UserIdId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_AspNetUsers_UserIdId",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "UserIdId",
                table: "User",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_User_UserIdId",
                table: "User",
                newName: "IX_User_ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_AspNetUsers_ApplicationUserId",
                table: "User",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
