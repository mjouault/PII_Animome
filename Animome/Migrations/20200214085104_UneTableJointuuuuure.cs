using Microsoft.EntityFrameworkCore.Migrations;

namespace Animome.Migrations
{
    public partial class UneTableJointuuuuure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Domaine_AspNetUsers_ApplicationUserId",
                table: "Domaine");

            migrationBuilder.DropForeignKey(
                name: "FK_Niveau_Prerequis_PrerequisId",
                table: "Niveau");

            migrationBuilder.DropForeignKey(
                name: "FK_Prerequis_Competence_LaCompetenceId",
                table: "Prerequis");

            migrationBuilder.DropIndex(
                name: "IX_Prerequis_LaCompetenceId",
                table: "Prerequis");

            migrationBuilder.DropIndex(
                name: "IX_Niveau_PrerequisId",
                table: "Niveau");

            migrationBuilder.DropIndex(
                name: "IX_Domaine_ApplicationUserId",
                table: "Domaine");

            migrationBuilder.DropColumn(
                name: "LaCompetenceId",
                table: "Prerequis");

            migrationBuilder.DropColumn(
                name: "PrerequisId",
                table: "Niveau");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Domaine");

            migrationBuilder.AddColumn<int>(
                name: "CompetenceId",
                table: "PatientCompetence",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "PatientCompetence",
                nullable: true);

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
                name: "DomaineUser",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    DomaineId = table.Column<int>(nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_PatientCompetence_CompetenceId",
                table: "PatientCompetence",
                column: "CompetenceId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientCompetence_PatientId",
                table: "PatientCompetence",
                column: "PatientId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_PatientCompetence_Competence_CompetenceId",
                table: "PatientCompetence",
                column: "CompetenceId",
                principalTable: "Competence",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientCompetence_Patient_PatientId",
                table: "PatientCompetence",
                column: "PatientId",
                principalTable: "Patient",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientCompetence_Competence_CompetenceId",
                table: "PatientCompetence");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientCompetence_Patient_PatientId",
                table: "PatientCompetence");

            migrationBuilder.DropTable(
                name: "CompetencePrerequis");

            migrationBuilder.DropTable(
                name: "DomaineUser");

            migrationBuilder.DropIndex(
                name: "IX_PatientCompetence_CompetenceId",
                table: "PatientCompetence");

            migrationBuilder.DropIndex(
                name: "IX_PatientCompetence_PatientId",
                table: "PatientCompetence");

            migrationBuilder.DropColumn(
                name: "CompetenceId",
                table: "PatientCompetence");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "PatientCompetence");

            migrationBuilder.AddColumn<int>(
                name: "LaCompetenceId",
                table: "Prerequis",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PrerequisId",
                table: "Niveau",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Domaine",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Prerequis_LaCompetenceId",
                table: "Prerequis",
                column: "LaCompetenceId");

            migrationBuilder.CreateIndex(
                name: "IX_Niveau_PrerequisId",
                table: "Niveau",
                column: "PrerequisId");

            migrationBuilder.CreateIndex(
                name: "IX_Domaine_ApplicationUserId",
                table: "Domaine",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Domaine_AspNetUsers_ApplicationUserId",
                table: "Domaine",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Niveau_Prerequis_PrerequisId",
                table: "Niveau",
                column: "PrerequisId",
                principalTable: "Prerequis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Prerequis_Competence_LaCompetenceId",
                table: "Prerequis",
                column: "LaCompetenceId",
                principalTable: "Competence",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
