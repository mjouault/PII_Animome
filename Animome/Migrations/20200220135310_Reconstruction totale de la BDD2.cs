using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Animome.Migrations
{
    public partial class ReconstructiontotaledelaBDD2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exercice",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Intitule = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercice", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Suivi",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(nullable: true),
                    DomaineId = table.Column<int>(nullable: true),
                    DernierExoModifieId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suivi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Suivi_Exercice_DernierExoModifieId",
                        column: x => x.DernierExoModifieId,
                        principalTable: "Exercice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Suivi_Domaine_DomaineId",
                        column: x => x.DomaineId,
                        principalTable: "Domaine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Suivi_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SuiviApplicationUser",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SuiviId = table.Column<int>(nullable: true),
                    ApplicationUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuiviApplicationUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SuiviApplicationUser_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SuiviApplicationUser_Suivi_SuiviId",
                        column: x => x.SuiviId,
                        principalTable: "Suivi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SuiviCompetence",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SuiviId = table.Column<int>(nullable: true),
                    CompetenceId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuiviCompetence", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SuiviCompetence_Competence_CompetenceId",
                        column: x => x.CompetenceId,
                        principalTable: "Competence",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SuiviCompetence_Suivi_SuiviId",
                        column: x => x.SuiviId,
                        principalTable: "Suivi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SuiviCompetencePrerequis",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrerequisId = table.Column<int>(nullable: true),
                    SuiviCompetenceId = table.Column<int>(nullable: true)
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
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SuiviCompetencePrerequisId = table.Column<int>(nullable: true),
                    NiveauId = table.Column<int>(nullable: true)
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
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nb = table.Column<int>(nullable: false),
                    NbFaits = table.Column<int>(nullable: false),
                    DateValidation = table.Column<DateTime>(nullable: false),
                    ValideurId = table.Column<string>(nullable: true),
                    Commentaire = table.Column<string>(nullable: true),
                    SuiviCompetencePrerequisNiveauId = table.Column<int>(nullable: true),
                    ExerciceId = table.Column<int>(nullable: true)
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
                name: "IX_Suivi_DernierExoModifieId",
                table: "Suivi",
                column: "DernierExoModifieId");

            migrationBuilder.CreateIndex(
                name: "IX_Suivi_DomaineId",
                table: "Suivi",
                column: "DomaineId");

            migrationBuilder.CreateIndex(
                name: "IX_Suivi_PatientId",
                table: "Suivi",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_SuiviApplicationUser_ApplicationUserId",
                table: "SuiviApplicationUser",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SuiviApplicationUser_SuiviId",
                table: "SuiviApplicationUser",
                column: "SuiviId");

            migrationBuilder.CreateIndex(
                name: "IX_SuiviCompetence_CompetenceId",
                table: "SuiviCompetence",
                column: "CompetenceId");

            migrationBuilder.CreateIndex(
                name: "IX_SuiviCompetence_SuiviId",
                table: "SuiviCompetence",
                column: "SuiviId");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SuiviApplicationUser");

            migrationBuilder.DropTable(
                name: "SuiviCompetencePrerequisNiveauExercice");

            migrationBuilder.DropTable(
                name: "SuiviCompetencePrerequisNiveau");

            migrationBuilder.DropTable(
                name: "SuiviCompetencePrerequis");

            migrationBuilder.DropTable(
                name: "SuiviCompetence");

            migrationBuilder.DropTable(
                name: "Suivi");

            migrationBuilder.DropTable(
                name: "Exercice");
        }
    }
}
