using Microsoft.EntityFrameworkCore.Migrations;

namespace Animome.Migrations
{
    public partial class CréationtablejointurePatientNiveau : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Complete",
                table: "Niveau");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Complete",
                table: "Niveau",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
