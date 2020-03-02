using Microsoft.EntityFrameworkCore.Migrations;

namespace Animome.Migrations
{
    public partial class ajoutDomaine2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Suivi_Domaine_DomaineId",
                table: "Suivi");

            migrationBuilder.DropIndex(
                name: "IX_Suivi_DomaineId",
                table: "Suivi");

            migrationBuilder.DropColumn(
                name: "DomaineId",
                table: "Suivi");

            migrationBuilder.AddColumn<int>(
                name: "Domaine2Id",
                table: "Suivi",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Suivi_Domaine2Id",
                table: "Suivi",
                column: "Domaine2Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Suivi_Domaine_Domaine2Id",
                table: "Suivi",
                column: "Domaine2Id",
                principalTable: "Domaine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Suivi_Domaine_Domaine2Id",
                table: "Suivi");

            migrationBuilder.DropIndex(
                name: "IX_Suivi_Domaine2Id",
                table: "Suivi");

            migrationBuilder.DropColumn(
                name: "Domaine2Id",
                table: "Suivi");

            migrationBuilder.AddColumn<int>(
                name: "DomaineId",
                table: "Suivi",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Suivi_DomaineId",
                table: "Suivi",
                column: "DomaineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Suivi_Domaine_DomaineId",
                table: "Suivi",
                column: "DomaineId",
                principalTable: "Domaine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
