using Microsoft.EntityFrameworkCore.Migrations;

namespace Animome.Migrations
{
    public partial class chgmtSUivi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Suivi_Domaine_Domaine2Id",
                table: "Suivi");

            migrationBuilder.DropIndex(
                name: "IX_Suivi_Domaine2Id",
                table: "Suivi");

            migrationBuilder.DropColumn(
                name: "Prerequis",
                table: "SuiviPrerequis");

            migrationBuilder.DropColumn(
                name: "Niveau",
                table: "SuiviNiveau");

            migrationBuilder.DropColumn(
                name: "Exercice",
                table: "SuiviExercice");

            migrationBuilder.DropColumn(
                name: "Competence",
                table: "SuiviCompetence");

            migrationBuilder.DropColumn(
                name: "Domaine",
                table: "Suivi");

            migrationBuilder.DropColumn(
                name: "Domaine2Id",
                table: "Suivi");

            migrationBuilder.AddColumn<int>(
                name: "DomaineId",
                table: "Suivi",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "Prerequis",
                table: "SuiviPrerequis",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Niveau",
                table: "SuiviNiveau",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Exercice",
                table: "SuiviExercice",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Competence",
                table: "SuiviCompetence",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Domaine",
                table: "Suivi",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Domaine2Id",
                table: "Suivi",
                type: "int",
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
    }
}
