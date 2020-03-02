using Microsoft.EntityFrameworkCore.Migrations;

namespace Animome.Migrations
{
    public partial class TablesJointuresDomainesCompetencesPrereNivExo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PrerequisId",
                table: "SuiviPrerequis",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NiveauId",
                table: "SuiviNiveau",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompetenceId",
                table: "SuiviCompetence",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DomaineId",
                table: "Suivi",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Competence",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Intitule = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competence", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Domaine",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Intitule = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Domaine", x => x.Id);
                });

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
                name: "Niveau",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Intitule = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Niveau", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Prerequis",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Intitule = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prerequis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DomaineCompetence",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DomaineId = table.Column<int>(nullable: true),
                    CompetenceId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DomaineCompetence", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DomaineCompetence_Competence_CompetenceId",
                        column: x => x.CompetenceId,
                        principalTable: "Competence",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DomaineCompetence_Domaine_DomaineId",
                        column: x => x.DomaineId,
                        principalTable: "Domaine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NiveauExercice",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NiveauId = table.Column<int>(nullable: true),
                    ExerciceId = table.Column<int>(nullable: true)
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

            migrationBuilder.CreateTable(
                name: "CompetencePrerequis",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompetenceId = table.Column<int>(nullable: true),
                    PrerequisId = table.Column<int>(nullable: true)
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
                name: "GetPrerequisNiveau",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NiveauId = table.Column<int>(nullable: true),
                    PrerequisId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GetPrerequisNiveau", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GetPrerequisNiveau_Niveau_NiveauId",
                        column: x => x.NiveauId,
                        principalTable: "Niveau",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GetPrerequisNiveau_Prerequis_PrerequisId",
                        column: x => x.PrerequisId,
                        principalTable: "Prerequis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SuiviPrerequis_PrerequisId",
                table: "SuiviPrerequis",
                column: "PrerequisId");

            migrationBuilder.CreateIndex(
                name: "IX_SuiviNiveau_NiveauId",
                table: "SuiviNiveau",
                column: "NiveauId");

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
                name: "IX_DomaineCompetence_CompetenceId",
                table: "DomaineCompetence",
                column: "CompetenceId");

            migrationBuilder.CreateIndex(
                name: "IX_DomaineCompetence_DomaineId",
                table: "DomaineCompetence",
                column: "DomaineId");

            migrationBuilder.CreateIndex(
                name: "IX_GetPrerequisNiveau_NiveauId",
                table: "GetPrerequisNiveau",
                column: "NiveauId");

            migrationBuilder.CreateIndex(
                name: "IX_GetPrerequisNiveau_PrerequisId",
                table: "GetPrerequisNiveau",
                column: "PrerequisId");

            migrationBuilder.CreateIndex(
                name: "IX_NiveauExercice_ExerciceId",
                table: "NiveauExercice",
                column: "ExerciceId");

            migrationBuilder.CreateIndex(
                name: "IX_NiveauExercice_NiveauId",
                table: "NiveauExercice",
                column: "NiveauId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_SuiviNiveau_Niveau_NiveauId",
                table: "SuiviNiveau",
                column: "NiveauId",
                principalTable: "Niveau",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SuiviPrerequis_Prerequis_PrerequisId",
                table: "SuiviPrerequis",
                column: "PrerequisId",
                principalTable: "Prerequis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Suivi_Domaine_DomaineId",
                table: "Suivi");

            migrationBuilder.DropForeignKey(
                name: "FK_SuiviCompetence_Competence_CompetenceId",
                table: "SuiviCompetence");

            migrationBuilder.DropForeignKey(
                name: "FK_SuiviNiveau_Niveau_NiveauId",
                table: "SuiviNiveau");

            migrationBuilder.DropForeignKey(
                name: "FK_SuiviPrerequis_Prerequis_PrerequisId",
                table: "SuiviPrerequis");

            migrationBuilder.DropTable(
                name: "CompetencePrerequis");

            migrationBuilder.DropTable(
                name: "DomaineCompetence");

            migrationBuilder.DropTable(
                name: "GetPrerequisNiveau");

            migrationBuilder.DropTable(
                name: "NiveauExercice");

            migrationBuilder.DropTable(
                name: "Competence");

            migrationBuilder.DropTable(
                name: "Domaine");

            migrationBuilder.DropTable(
                name: "Prerequis");

            migrationBuilder.DropTable(
                name: "Exercice");

            migrationBuilder.DropTable(
                name: "Niveau");

            migrationBuilder.DropIndex(
                name: "IX_SuiviPrerequis_PrerequisId",
                table: "SuiviPrerequis");

            migrationBuilder.DropIndex(
                name: "IX_SuiviNiveau_NiveauId",
                table: "SuiviNiveau");

            migrationBuilder.DropIndex(
                name: "IX_SuiviCompetence_CompetenceId",
                table: "SuiviCompetence");

            migrationBuilder.DropIndex(
                name: "IX_Suivi_DomaineId",
                table: "Suivi");

            migrationBuilder.DropColumn(
                name: "PrerequisId",
                table: "SuiviPrerequis");

            migrationBuilder.DropColumn(
                name: "NiveauId",
                table: "SuiviNiveau");

            migrationBuilder.DropColumn(
                name: "CompetenceId",
                table: "SuiviCompetence");

            migrationBuilder.DropColumn(
                name: "DomaineId",
                table: "Suivi");
        }
    }
}
