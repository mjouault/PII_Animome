using Microsoft.EntityFrameworkCore.Migrations;

namespace Animome.Migrations
{
    public partial class rsolpbValideurSuiviExo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SuiviExercice_AspNetUsers_ValideurId",
                table: "SuiviExercice");

            migrationBuilder.DropIndex(
                name: "IX_SuiviExercice_ValideurId",
                table: "SuiviExercice");

            migrationBuilder.DropColumn(
                name: "ValideurId",
                table: "SuiviExercice");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "SuiviExercice",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SuiviExercice_ApplicationUserId",
                table: "SuiviExercice",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SuiviExercice_AspNetUsers_ApplicationUserId",
                table: "SuiviExercice",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SuiviExercice_AspNetUsers_ApplicationUserId",
                table: "SuiviExercice");

            migrationBuilder.DropIndex(
                name: "IX_SuiviExercice_ApplicationUserId",
                table: "SuiviExercice");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "SuiviExercice");

            migrationBuilder.AddColumn<string>(
                name: "ValideurId",
                table: "SuiviExercice",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SuiviExercice_ValideurId",
                table: "SuiviExercice",
                column: "ValideurId");

            migrationBuilder.AddForeignKey(
                name: "FK_SuiviExercice_AspNetUsers_ValideurId",
                table: "SuiviExercice",
                column: "ValideurId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
