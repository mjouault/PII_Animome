using Microsoft.EntityFrameworkCore.Migrations;

namespace Animome.Migrations
{
    public partial class ModifPatient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Identifiant",
                table: "Patient",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Identifiant",
                table: "Patient");
        }
    }
}
