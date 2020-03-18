using Microsoft.EntityFrameworkCore.Migrations;

namespace Animome.Migrations
{
    public partial class TentativeSupreme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Valide",
                table: "SuiviPrerequis");

            migrationBuilder.DropColumn(
                name: "Valide",
                table: "SuiviNiveau");

            migrationBuilder.DropColumn(
                name: "Valide",
                table: "SuiviCompetence");

            migrationBuilder.DropColumn(
                name: "Valide",
                table: "Suivi");

            migrationBuilder.DropColumn(
                name: "Identifiant",
                table: "Patient");

            migrationBuilder.AddColumn<int>(
                name: "Etat",
                table: "SuiviPrerequis",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Etat",
                table: "SuiviNiveau",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Etat",
                table: "SuiviCompetence",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Etat",
                table: "Suivi",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Numero",
                table: "Patient",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Commentaire",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SuiviApplicationUserId = table.Column<int>(nullable: true),
                    Texte = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commentaire", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Commentaire_SuiviApplicationUser_SuiviApplicationUserId",
                        column: x => x.SuiviApplicationUserId,
                        principalTable: "SuiviApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Commentaire_SuiviApplicationUserId",
                table: "Commentaire",
                column: "SuiviApplicationUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Commentaire");

            migrationBuilder.DropColumn(
                name: "Etat",
                table: "SuiviPrerequis");

            migrationBuilder.DropColumn(
                name: "Etat",
                table: "SuiviNiveau");

            migrationBuilder.DropColumn(
                name: "Etat",
                table: "SuiviCompetence");

            migrationBuilder.DropColumn(
                name: "Etat",
                table: "Suivi");

            migrationBuilder.DropColumn(
                name: "Numero",
                table: "Patient");

            migrationBuilder.AddColumn<bool>(
                name: "Valide",
                table: "SuiviPrerequis",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Valide",
                table: "SuiviNiveau",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Valide",
                table: "SuiviCompetence",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Valide",
                table: "Suivi",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Identifiant",
                table: "Patient",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
