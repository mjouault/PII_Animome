using Microsoft.EntityFrameworkCore.Migrations;

namespace Animome.Migrations
{
    public partial class suppressionSuiviuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SuiviApplicationUser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SuiviApplicationUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SuiviId = table.Column<int>(type: "int", nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_SuiviApplicationUser_ApplicationUserId",
                table: "SuiviApplicationUser",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SuiviApplicationUser_SuiviId",
                table: "SuiviApplicationUser",
                column: "SuiviId");
        }
    }
}
