using Microsoft.EntityFrameworkCore.Migrations;

namespace Animome.Migrations
{
    public partial class ChgmtNomPrerequisNiveau : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GetPrerequisNiveau_Niveau_NiveauId",
                table: "GetPrerequisNiveau");

            migrationBuilder.DropForeignKey(
                name: "FK_GetPrerequisNiveau_Prerequis_PrerequisId",
                table: "GetPrerequisNiveau");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GetPrerequisNiveau",
                table: "GetPrerequisNiveau");

            migrationBuilder.RenameTable(
                name: "GetPrerequisNiveau",
                newName: "PrerequisNiveau");

            migrationBuilder.RenameIndex(
                name: "IX_GetPrerequisNiveau_PrerequisId",
                table: "PrerequisNiveau",
                newName: "IX_PrerequisNiveau_PrerequisId");

            migrationBuilder.RenameIndex(
                name: "IX_GetPrerequisNiveau_NiveauId",
                table: "PrerequisNiveau",
                newName: "IX_PrerequisNiveau_NiveauId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PrerequisNiveau",
                table: "PrerequisNiveau",
                column: "Id");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrerequisNiveau_Niveau_NiveauId",
                table: "PrerequisNiveau");

            migrationBuilder.DropForeignKey(
                name: "FK_PrerequisNiveau_Prerequis_PrerequisId",
                table: "PrerequisNiveau");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PrerequisNiveau",
                table: "PrerequisNiveau");

            migrationBuilder.RenameTable(
                name: "PrerequisNiveau",
                newName: "GetPrerequisNiveau");

            migrationBuilder.RenameIndex(
                name: "IX_PrerequisNiveau_PrerequisId",
                table: "GetPrerequisNiveau",
                newName: "IX_GetPrerequisNiveau_PrerequisId");

            migrationBuilder.RenameIndex(
                name: "IX_PrerequisNiveau_NiveauId",
                table: "GetPrerequisNiveau",
                newName: "IX_GetPrerequisNiveau_NiveauId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GetPrerequisNiveau",
                table: "GetPrerequisNiveau",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GetPrerequisNiveau_Niveau_NiveauId",
                table: "GetPrerequisNiveau",
                column: "NiveauId",
                principalTable: "Niveau",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GetPrerequisNiveau_Prerequis_PrerequisId",
                table: "GetPrerequisNiveau",
                column: "PrerequisId",
                principalTable: "Prerequis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
