using Microsoft.EntityFrameworkCore.Migrations;

namespace Animome.Migrations
{
    public partial class Resol_pb_Utilisateur : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Domaine_Utilisateur_UtilisateurId",
                table: "Domaine");

            migrationBuilder.DropTable(
                name: "Utilisateur");

            migrationBuilder.DropIndex(
                name: "IX_Domaine_UtilisateurId",
                table: "Domaine");

            migrationBuilder.DropColumn(
                name: "UtilisateurId",
                table: "Domaine");

            migrationBuilder.DropColumn(
                name: "Genre",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Domaine",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Prenom",
                table: "AspNetUsers",
                nullable: true);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Domaine_AspNetUsers_ApplicationUserId",
                table: "Domaine");

            migrationBuilder.DropIndex(
                name: "IX_Domaine_ApplicationUserId",
                table: "Domaine");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Domaine");

            migrationBuilder.DropColumn(
                name: "Prenom",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "UtilisateurId",
                table: "Domaine",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Utilisateur",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilisateur", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Domaine_UtilisateurId",
                table: "Domaine",
                column: "UtilisateurId");

            migrationBuilder.AddForeignKey(
                name: "FK_Domaine_Utilisateur_UtilisateurId",
                table: "Domaine",
                column: "UtilisateurId",
                principalTable: "Utilisateur",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
