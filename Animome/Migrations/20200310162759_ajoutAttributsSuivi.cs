using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Animome.Migrations
{
    public partial class ajoutAttributsSuivi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateValide",
                table: "Suivi",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Valide",
                table: "Suivi",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateValide",
                table: "Suivi");

            migrationBuilder.DropColumn(
                name: "Valide",
                table: "Suivi");
        }
    }
}
