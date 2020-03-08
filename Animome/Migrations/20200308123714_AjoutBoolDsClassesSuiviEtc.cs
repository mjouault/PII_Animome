using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Animome.Migrations
{
    public partial class AjoutBoolDsClassesSuiviEtc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Suivi_SuiviExercice_DernierExoModifieId",
                table: "Suivi");

            migrationBuilder.DropTable(
                name: "NiveauExercice");

            migrationBuilder.DropTable(
                name: "Exercice");

            migrationBuilder.DropIndex(
                name: "IX_Suivi_DernierExoModifieId",
                table: "Suivi");

            migrationBuilder.DropColumn(
                name: "Nb",
                table: "SuiviNiveau");

            migrationBuilder.DropColumn(
                name: "Commentaire",
                table: "SuiviExercice");

            migrationBuilder.DropColumn(
                name: "Fait",
                table: "SuiviExercice");

            migrationBuilder.DropColumn(
                name: "DernierExoModifieId",
                table: "Suivi");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateValide",
                table: "SuiviPrerequis",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Valide",
                table: "SuiviPrerequis",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateValide",
                table: "SuiviNiveau",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Valide",
                table: "SuiviNiveau",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Valide",
                table: "SuiviExercice",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateValide",
                table: "SuiviCompetence",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Valide",
                table: "SuiviCompetence",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateValide",
                table: "SuiviPrerequis");

            migrationBuilder.DropColumn(
                name: "Valide",
                table: "SuiviPrerequis");

            migrationBuilder.DropColumn(
                name: "DateValide",
                table: "SuiviNiveau");

            migrationBuilder.DropColumn(
                name: "Valide",
                table: "SuiviNiveau");

            migrationBuilder.DropColumn(
                name: "Valide",
                table: "SuiviExercice");

            migrationBuilder.DropColumn(
                name: "DateValide",
                table: "SuiviCompetence");

            migrationBuilder.DropColumn(
                name: "Valide",
                table: "SuiviCompetence");

            migrationBuilder.AddColumn<int>(
                name: "Nb",
                table: "SuiviNiveau",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Commentaire",
                table: "SuiviExercice",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Fait",
                table: "SuiviExercice",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "DernierExoModifieId",
                table: "Suivi",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Exercice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Intitule = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercice", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NiveauExercice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExerciceId = table.Column<int>(type: "int", nullable: true),
                    NiveauId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NiveauExercice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NiveauExercice_Exercice_ExerciceId",
                        column: x => x.ExerciceId,
                        principalTable: "Exercice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NiveauExercice_Niveau_NiveauId",
                        column: x => x.NiveauId,
                        principalTable: "Niveau",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Suivi_DernierExoModifieId",
                table: "Suivi",
                column: "DernierExoModifieId");

            migrationBuilder.CreateIndex(
                name: "IX_NiveauExercice_ExerciceId",
                table: "NiveauExercice",
                column: "ExerciceId");

            migrationBuilder.CreateIndex(
                name: "IX_NiveauExercice_NiveauId",
                table: "NiveauExercice",
                column: "NiveauId");

            migrationBuilder.AddForeignKey(
                name: "FK_Suivi_SuiviExercice_DernierExoModifieId",
                table: "Suivi",
                column: "DernierExoModifieId",
                principalTable: "SuiviExercice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
