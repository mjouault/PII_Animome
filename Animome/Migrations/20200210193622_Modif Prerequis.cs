using Microsoft.EntityFrameworkCore.Migrations;

namespace Animome.Migrations
{
    public partial class ModifPrerequis : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prerequis_Competence_CompetenceId",
                table: "Prerequis");

            migrationBuilder.DropIndex(
                name: "IX_Prerequis_CompetenceId",
                table: "Prerequis");

            migrationBuilder.DropColumn(
                name: "CompetenceId",
                table: "Prerequis");

            migrationBuilder.AddColumn<int>(
                name: "LaCompetenceId",
                table: "Prerequis",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Prerequis_LaCompetenceId",
                table: "Prerequis",
                column: "LaCompetenceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prerequis_Competence_LaCompetenceId",
                table: "Prerequis",
                column: "LaCompetenceId",
                principalTable: "Competence",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prerequis_Competence_LaCompetenceId",
                table: "Prerequis");

            migrationBuilder.DropIndex(
                name: "IX_Prerequis_LaCompetenceId",
                table: "Prerequis");

            migrationBuilder.DropColumn(
                name: "LaCompetenceId",
                table: "Prerequis");

            migrationBuilder.AddColumn<int>(
                name: "CompetenceId",
                table: "Prerequis",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Prerequis_CompetenceId",
                table: "Prerequis",
                column: "CompetenceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prerequis_Competence_CompetenceId",
                table: "Prerequis",
                column: "CompetenceId",
                principalTable: "Competence",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
