using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PHIMS.Migrations
{
    public partial class ffr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Result_Pateint_ID",
                table: "Result",
                column: "Pateint_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Result_Pateint_Pateint_ID",
                table: "Result",
                column: "Pateint_ID",
                principalTable: "Pateint",
                principalColumn: "Pateint_Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Result_Pateint_Pateint_ID",
                table: "Result");

            migrationBuilder.DropIndex(
                name: "IX_Result_Pateint_ID",
                table: "Result");
        }
    }
}
