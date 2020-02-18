using Microsoft.EntityFrameworkCore.Migrations;

namespace Animome.Migrations
{
    public partial class EnumDomaine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LesDomaines",
                table: "Domaine",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LesDomaines",
                table: "Domaine");
        }
    }
}
