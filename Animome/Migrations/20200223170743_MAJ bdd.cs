using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Animome.Migrations
{
    public partial class MAJbdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Suivi_Exercice_DernierExoModifieId",
                table: "Suivi");

            migrationBuilder.DropForeignKey(
                name: "FK_Suivi_Domaine_DomaineId",
                table: "Suivi");

            migrationBuilder.DropForeignKey(
                name: "FK_SuiviCompetence_Competence_CompetenceId",
                table: "SuiviCompetence");

            migrationBuilder.DropTable(
                name: "CompetencePrerequis");

            migrationBuilder.DropTable(
                name: "DomaineUser");

            migrationBuilder.DropTable(
                name: "PatientCompetence");

            migrationBuilder.DropTable(
                name: "SuiviCompetencePrerequisNiveauExercice");

            migrationBuilder.DropTable(
                name: "Domaine");

            migrationBuilder.DropTable(
                name: "Competence");

            migrationBuilder.DropTable(
                name: "Exercice");

            migrationBuilder.DropTable(
                name: "SuiviCompetencePrerequisNiveau");

            migrationBuilder.DropTable(
                name: "Niveau");

            migrationBuilder.DropTable(
                name: "SuiviCompetencePrerequis");

            migrationBuilder.DropTable(
                name: "Prerequis");

            migrationBuilder.DropIndex(
                name: "IX_SuiviCompetence_CompetenceId",
                table: "SuiviCompetence");

            migrationBuilder.DropIndex(
                name: "IX_Suivi_DomaineId",
                table: "Suivi");

            migrationBuilder.DropColumn(
                name: "CompetenceId",
                table: "SuiviCompetence");

            migrationBuilder.DropColumn(
                name: "DomaineId",
                table: "Suivi");

            migrationBuilder.AddColumn<int>(
                name: "Competence",
                table: "SuiviCompetence",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Domaine",
                table: "Suivi",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SuiviPrerequis",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SuiviCompetenceId = table.Column<int>(nullable: true),
                    Prerequis = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuiviPrerequis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SuiviPrerequis_SuiviCompetence_SuiviCompetenceId",
                        column: x => x.SuiviCompetenceId,
                        principalTable: "SuiviCompetence",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SuiviNiveau",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SuiviPrerequisId = table.Column<int>(nullable: true),
                    Niveau = table.Column<int>(nullable: false),
                    Nb = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuiviNiveau", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SuiviNiveau_SuiviPrerequis_SuiviPrerequisId",
                        column: x => x.SuiviPrerequisId,
                        principalTable: "SuiviPrerequis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SuiviExercice",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fait = table.Column<bool>(nullable: false),
                    DateFait = table.Column<DateTime>(nullable: false),
                    DateValide = table.Column<DateTime>(nullable: false),
                    ValideurId = table.Column<string>(nullable: true),
                    Commentaire = table.Column<string>(nullable: true),
                    SuiviNiveauId = table.Column<int>(nullable: true),
                    Exercice = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuiviExercice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SuiviExercice_SuiviNiveau_SuiviNiveauId",
                        column: x => x.SuiviNiveauId,
                        principalTable: "SuiviNiveau",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SuiviExercice_AspNetUsers_ValideurId",
                        column: x => x.ValideurId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SuiviExercice_SuiviNiveauId",
                table: "SuiviExercice",
                column: "SuiviNiveauId");

            migrationBuilder.CreateIndex(
                name: "IX_SuiviExercice_ValideurId",
                table: "SuiviExercice",
                column: "ValideurId");

            migrationBuilder.CreateIndex(
                name: "IX_SuiviNiveau_SuiviPrerequisId",
                table: "SuiviNiveau",
                column: "SuiviPrerequisId");

            migrationBuilder.CreateIndex(
                name: "IX_SuiviPrerequis_SuiviCompetenceId",
                table: "SuiviPrerequis",
                column: "SuiviCompetenceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Suivi_SuiviExercice_DernierExoModifieId",
                table: "Suivi",
                column: "DernierExoModifieId",
                principalTable: "SuiviExercice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Suivi_SuiviExercice_DernierExoModifieId",
                table: "Suivi");

            migrationBuilder.DropTable(
                name: "SuiviExercice");

            migrationBuilder.DropTable(
                name: "SuiviNiveau");

            migrationBuilder.DropTable(
                name: "SuiviPrerequis");

            migrationBuilder.DropColumn(
                name: "Competence",
                table: "SuiviCompetence");

            migrationBuilder.DropColumn(
                name: "Domaine",
                table: "Suivi");

            migrationBuilder.AddColumn<int>(
                name: "CompetenceId",
                table: "SuiviCompetence",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DomaineId",
                table: "Suivi",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Competence",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Intitule = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competence", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Domaine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Intitule = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LesDomaines = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Domaine", x => x.Id);
                });

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
                name: "Niveau",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Intitule = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Niveau", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Prerequis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Intitule = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prerequis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatientCompetence",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompetenceId = table.Column<int>(type: "int", nullable: true),
                    PatientId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientCompetence", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientCompetence_Competence_CompetenceId",
                        column: x => x.CompetenceId,
                        principalTable: "Competence",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PatientCompetence_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DomaineUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DomaineId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DomaineUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DomaineUser_Domaine_DomaineId",
                        column: x => x.DomaineId,
                        principalTable: "Domaine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DomaineUser_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompetencePrerequis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompetenceId = table.Column<int>(type: "int", nullable: true),
                    PrerequisId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetencePrerequis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompetencePrerequis_Competence_CompetenceId",
                        column: x => x.CompetenceId,
                        principalTable: "Competence",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompetencePrerequis_Prerequis_PrerequisId",
                        column: x => x.PrerequisId,
                        principalTable: "Prerequis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SuiviCompetencePrerequis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrerequisId = table.Column<int>(type: "int", nullable: true),
                    SuiviCompetenceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuiviCompetencePrerequis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SuiviCompetencePrerequis_Prerequis_PrerequisId",
                        column: x => x.PrerequisId,
                        principalTable: "Prerequis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SuiviCompetencePrerequis_SuiviCompetence_SuiviCompetenceId",
                        column: x => x.SuiviCompetenceId,
                        principalTable: "SuiviCompetence",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SuiviCompetencePrerequisNiveau",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NiveauId = table.Column<int>(type: "int", nullable: true),
                    SuiviCompetencePrerequisId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuiviCompetencePrerequisNiveau", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SuiviCompetencePrerequisNiveau_Niveau_NiveauId",
                        column: x => x.NiveauId,
                        principalTable: "Niveau",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SuiviCompetencePrerequisNiveau_SuiviCompetencePrerequis_SuiviCompetencePrerequisId",
                        column: x => x.SuiviCompetencePrerequisId,
                        principalTable: "SuiviCompetencePrerequis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SuiviCompetencePrerequisNiveauExercice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Commentaire = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateValidation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExerciceId = table.Column<int>(type: "int", nullable: true),
                    Nb = table.Column<int>(type: "int", nullable: false),
                    NbFaits = table.Column<int>(type: "int", nullable: false),
                    SuiviCompetencePrerequisNiveauId = table.Column<int>(type: "int", nullable: true),
                    ValideurId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuiviCompetencePrerequisNiveauExercice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SuiviCompetencePrerequisNiveauExercice_Exercice_ExerciceId",
                        column: x => x.ExerciceId,
                        principalTable: "Exercice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SuiviCompetencePrerequisNiveauExercice_SuiviCompetencePrerequisNiveau_SuiviCompetencePrerequisNiveauId",
                        column: x => x.SuiviCompetencePrerequisNiveauId,
                        principalTable: "SuiviCompetencePrerequisNiveau",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SuiviCompetencePrerequisNiveauExercice_AspNetUsers_ValideurId",
                        column: x => x.ValideurId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SuiviCompetence_CompetenceId",
                table: "SuiviCompetence",
                column: "CompetenceId");

            migrationBuilder.CreateIndex(
                name: "IX_Suivi_DomaineId",
                table: "Suivi",
                column: "DomaineId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetencePrerequis_CompetenceId",
                table: "CompetencePrerequis",
                column: "CompetenceId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetencePrerequis_PrerequisId",
                table: "CompetencePrerequis",
                column: "PrerequisId");

            migrationBuilder.CreateIndex(
                name: "IX_DomaineUser_DomaineId",
                table: "DomaineUser",
                column: "DomaineId");

            migrationBuilder.CreateIndex(
                name: "IX_DomaineUser_UserId",
                table: "DomaineUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientCompetence_CompetenceId",
                table: "PatientCompetence",
                column: "CompetenceId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientCompetence_PatientId",
                table: "PatientCompetence",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_SuiviCompetencePrerequis_PrerequisId",
                table: "SuiviCompetencePrerequis",
                column: "PrerequisId");

            migrationBuilder.CreateIndex(
                name: "IX_SuiviCompetencePrerequis_SuiviCompetenceId",
                table: "SuiviCompetencePrerequis",
                column: "SuiviCompetenceId");

            migrationBuilder.CreateIndex(
                name: "IX_SuiviCompetencePrerequisNiveau_NiveauId",
                table: "SuiviCompetencePrerequisNiveau",
                column: "NiveauId");

            migrationBuilder.CreateIndex(
                name: "IX_SuiviCompetencePrerequisNiveau_SuiviCompetencePrerequisId",
                table: "SuiviCompetencePrerequisNiveau",
                column: "SuiviCompetencePrerequisId");

            migrationBuilder.CreateIndex(
                name: "IX_SuiviCompetencePrerequisNiveauExercice_ExerciceId",
                table: "SuiviCompetencePrerequisNiveauExercice",
                column: "ExerciceId");

            migrationBuilder.CreateIndex(
                name: "IX_SuiviCompetencePrerequisNiveauExercice_SuiviCompetencePrerequisNiveauId",
                table: "SuiviCompetencePrerequisNiveauExercice",
                column: "SuiviCompetencePrerequisNiveauId");

            migrationBuilder.CreateIndex(
                name: "IX_SuiviCompetencePrerequisNiveauExercice_ValideurId",
                table: "SuiviCompetencePrerequisNiveauExercice",
                column: "ValideurId");

            migrationBuilder.AddForeignKey(
                name: "FK_Suivi_Exercice_DernierExoModifieId",
                table: "Suivi",
                column: "DernierExoModifieId",
                principalTable: "Exercice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Suivi_Domaine_DomaineId",
                table: "Suivi",
                column: "DomaineId",
                principalTable: "Domaine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SuiviCompetence_Competence_CompetenceId",
                table: "SuiviCompetence",
                column: "CompetenceId",
                principalTable: "Competence",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
