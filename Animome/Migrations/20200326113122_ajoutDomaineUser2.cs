using Microsoft.EntityFrameworkCore.Migrations;

namespace Animome.Migrations
{
    public partial class ajoutDomaineUser2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DomaineUser",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DomaineId = table.Column<int>(nullable: true),
                    ApplicationUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DomaineUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DomaineUser_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DomaineUser_Domaine_DomaineId",
                        column: x => x.DomaineId,
                        principalTable: "Domaine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DomaineUser_ApplicationUserId",
                table: "DomaineUser",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DomaineUser_DomaineId",
                table: "DomaineUser",
                column: "DomaineId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DomaineUser");

            migrationBuilder.AddColumn<string>(
                name: "Essai",
                table: "SuiviExercice",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
