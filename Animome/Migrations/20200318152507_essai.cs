using Microsoft.EntityFrameworkCore.Migrations;

namespace Animome.Migrations
{
    public partial class essai : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Essai",
                table: "SuiviExercice",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Essai",
                table: "SuiviExercice");
        }
    }
}
