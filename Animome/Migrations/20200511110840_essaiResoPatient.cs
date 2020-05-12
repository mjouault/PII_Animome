using Microsoft.EntityFrameworkCore.Migrations;

namespace Animome.Migrations
{
    public partial class essaiResoPatient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompetencePrerequis_Competence_CompetenceId",
                table: "CompetencePrerequis");

            migrationBuilder.DropForeignKey(
                name: "FK_CompetencePrerequis_Prerequis_PrerequisId",
                table: "CompetencePrerequis");

            migrationBuilder.DropForeignKey(
                name: "FK_DomaineCompetence_Competence_CompetenceId",
                table: "DomaineCompetence");

            migrationBuilder.DropForeignKey(
                name: "FK_DomaineCompetence_Domaine_DomaineId",
                table: "DomaineCompetence");

            migrationBuilder.DropForeignKey(
                name: "FK_DomaineUser_Domaine_DomaineId",
                table: "DomaineUser");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientUser_AspNetUsers_ApplicationUserId",
                table: "PatientUser");

            migrationBuilder.DropForeignKey(
                name: "FK_PrerequisNiveau_Niveau_NiveauId",
                table: "PrerequisNiveau");

            migrationBuilder.DropForeignKey(
                name: "FK_PrerequisNiveau_Prerequis_PrerequisId",
                table: "PrerequisNiveau");

            migrationBuilder.AlterColumn<int>(
                name: "PrerequisId",
                table: "PrerequisNiveau",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NiveauId",
                table: "PrerequisNiveau",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Intitule",
                table: "Prerequis",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "PatientUser",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Texte",
                table: "Note",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Intitule",
                table: "Niveau",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DomaineId",
                table: "DomaineUser",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DomaineId",
                table: "DomaineCompetence",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CompetenceId",
                table: "DomaineCompetence",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Intitule",
                table: "Domaine",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PrerequisId",
                table: "CompetencePrerequis",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CompetenceId",
                table: "CompetencePrerequis",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Intitule",
                table: "Competence",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Prenom",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nom",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CompetencePrerequis_Competence_CompetenceId",
                table: "CompetencePrerequis",
                column: "CompetenceId",
                principalTable: "Competence",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompetencePrerequis_Prerequis_PrerequisId",
                table: "CompetencePrerequis",
                column: "PrerequisId",
                principalTable: "Prerequis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DomaineCompetence_Competence_CompetenceId",
                table: "DomaineCompetence",
                column: "CompetenceId",
                principalTable: "Competence",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DomaineCompetence_Domaine_DomaineId",
                table: "DomaineCompetence",
                column: "DomaineId",
                principalTable: "Domaine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DomaineUser_Domaine_DomaineId",
                table: "DomaineUser",
                column: "DomaineId",
                principalTable: "Domaine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientUser_AspNetUsers_ApplicationUserId",
                table: "PatientUser",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PrerequisNiveau_Niveau_NiveauId",
                table: "PrerequisNiveau",
                column: "NiveauId",
                principalTable: "Niveau",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PrerequisNiveau_Prerequis_PrerequisId",
                table: "PrerequisNiveau",
                column: "PrerequisId",
                principalTable: "Prerequis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompetencePrerequis_Competence_CompetenceId",
                table: "CompetencePrerequis");

            migrationBuilder.DropForeignKey(
                name: "FK_CompetencePrerequis_Prerequis_PrerequisId",
                table: "CompetencePrerequis");

            migrationBuilder.DropForeignKey(
                name: "FK_DomaineCompetence_Competence_CompetenceId",
                table: "DomaineCompetence");

            migrationBuilder.DropForeignKey(
                name: "FK_DomaineCompetence_Domaine_DomaineId",
                table: "DomaineCompetence");

            migrationBuilder.DropForeignKey(
                name: "FK_DomaineUser_Domaine_DomaineId",
                table: "DomaineUser");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientUser_AspNetUsers_ApplicationUserId",
                table: "PatientUser");

            migrationBuilder.DropForeignKey(
                name: "FK_PrerequisNiveau_Niveau_NiveauId",
                table: "PrerequisNiveau");

            migrationBuilder.DropForeignKey(
                name: "FK_PrerequisNiveau_Prerequis_PrerequisId",
                table: "PrerequisNiveau");

            migrationBuilder.AlterColumn<int>(
                name: "PrerequisId",
                table: "PrerequisNiveau",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "NiveauId",
                table: "PrerequisNiveau",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Intitule",
                table: "Prerequis",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "PatientUser",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Texte",
                table: "Note",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Intitule",
                table: "Niveau",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "DomaineId",
                table: "DomaineUser",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "DomaineId",
                table: "DomaineCompetence",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "CompetenceId",
                table: "DomaineCompetence",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Intitule",
                table: "Domaine",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "PrerequisId",
                table: "CompetencePrerequis",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "CompetenceId",
                table: "CompetencePrerequis",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Intitule",
                table: "Competence",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Prenom",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Nom",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_CompetencePrerequis_Competence_CompetenceId",
                table: "CompetencePrerequis",
                column: "CompetenceId",
                principalTable: "Competence",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CompetencePrerequis_Prerequis_PrerequisId",
                table: "CompetencePrerequis",
                column: "PrerequisId",
                principalTable: "Prerequis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DomaineCompetence_Competence_CompetenceId",
                table: "DomaineCompetence",
                column: "CompetenceId",
                principalTable: "Competence",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DomaineCompetence_Domaine_DomaineId",
                table: "DomaineCompetence",
                column: "DomaineId",
                principalTable: "Domaine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DomaineUser_Domaine_DomaineId",
                table: "DomaineUser",
                column: "DomaineId",
                principalTable: "Domaine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientUser_AspNetUsers_ApplicationUserId",
                table: "PatientUser",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PrerequisNiveau_Niveau_NiveauId",
                table: "PrerequisNiveau",
                column: "NiveauId",
                principalTable: "Niveau",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PrerequisNiveau_Prerequis_PrerequisId",
                table: "PrerequisNiveau",
                column: "PrerequisId",
                principalTable: "Prerequis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
