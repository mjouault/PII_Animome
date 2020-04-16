using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Animome.Migrations
{
    public partial class InitialPerso : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Prenom = table.Column<string>(nullable: true),
                    Nom = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Competence",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Intitule = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competence", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Domaine",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Intitule = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Domaine", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Niveau",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Intitule = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Niveau", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Patient",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Prerequis",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Intitule = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prerequis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DomaineCompetence",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DomaineId = table.Column<int>(nullable: true),
                    CompetenceId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DomaineCompetence", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DomaineCompetence_Competence_CompetenceId",
                        column: x => x.CompetenceId,
                        principalTable: "Competence",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DomaineCompetence_Domaine_DomaineId",
                        column: x => x.DomaineId,
                        principalTable: "Domaine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateTable(
                name: "PatientUser",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(nullable: true),
                    ApplicationUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientUser_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PatientUser_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Suivi",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(nullable: true),
                    DomaineId = table.Column<int>(nullable: true),
                    Etat = table.Column<int>(nullable: false),
                    DateValide = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suivi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Suivi_Domaine_DomaineId",
                        column: x => x.DomaineId,
                        principalTable: "Domaine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Suivi_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompetencePrerequis",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompetenceId = table.Column<int>(nullable: true),
                    PrerequisId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetencePrerequis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompetencePrerequis_Competence_CompetenceId",
                        column: x => x.CompetenceId,
                        principalTable: "Competence",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompetencePrerequis_Prerequis_PrerequisId",
                        column: x => x.PrerequisId,
                        principalTable: "Prerequis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PrerequisNiveau",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NiveauId = table.Column<int>(nullable: true),
                    PrerequisId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrerequisNiveau", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrerequisNiveau_Niveau_NiveauId",
                        column: x => x.NiveauId,
                        principalTable: "Niveau",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrerequisNiveau_Prerequis_PrerequisId",
                        column: x => x.PrerequisId,
                        principalTable: "Prerequis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SuiviApplicationUser",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SuiviId = table.Column<int>(nullable: true),
                    ApplicationUserId = table.Column<string>(nullable: true)
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

            migrationBuilder.CreateTable(
                name: "SuiviCompetence",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SuiviId = table.Column<int>(nullable: true),
                    CompetenceId = table.Column<int>(nullable: true),
                    Etat = table.Column<int>(nullable: false),
                    DateValide = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuiviCompetence", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SuiviCompetence_Competence_CompetenceId",
                        column: x => x.CompetenceId,
                        principalTable: "Competence",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SuiviCompetence_Suivi_SuiviId",
                        column: x => x.SuiviId,
                        principalTable: "Suivi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SuiviPrerequis",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SuiviCompetenceId = table.Column<int>(nullable: true),
                    PrerequisId = table.Column<int>(nullable: true),
                    Etat = table.Column<int>(nullable: false),
                    DateValide = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuiviPrerequis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SuiviPrerequis_Prerequis_PrerequisId",
                        column: x => x.PrerequisId,
                        principalTable: "Prerequis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SuiviPrerequis_SuiviCompetence_SuiviCompetenceId",
                        column: x => x.SuiviCompetenceId,
                        principalTable: "SuiviCompetence",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SuiviNiveau",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SuiviPrerequisId = table.Column<int>(nullable: true),
                    NiveauId = table.Column<int>(nullable: true),
                    Etat = table.Column<int>(nullable: false),
                    DateValide = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuiviNiveau", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SuiviNiveau_Niveau_NiveauId",
                        column: x => x.NiveauId,
                        principalTable: "Niveau",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SuiviNiveau_SuiviPrerequis_SuiviPrerequisId",
                        column: x => x.SuiviPrerequisId,
                        principalTable: "SuiviPrerequis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Note",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    SuiviNiveauId = table.Column<int>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Texte = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Note", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Note_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Note_SuiviNiveau_SuiviNiveauId",
                        column: x => x.SuiviNiveauId,
                        principalTable: "SuiviNiveau",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SuiviExercice",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Valide = table.Column<bool>(nullable: false),
                    DateFait = table.Column<DateTime>(nullable: false),
                    DateValide = table.Column<DateTime>(nullable: false),
                    ValideurId = table.Column<string>(nullable: true),
                    SuiviNiveauId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuiviExercice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SuiviExercice_SuiviNiveau_SuiviNiveauId",
                        column: x => x.SuiviNiveauId,
                        principalTable: "SuiviNiveau",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SuiviExercice_AspNetUsers_ValideurId",
                        column: x => x.ValideurId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CompetencePrerequis_CompetenceId",
                table: "CompetencePrerequis",
                column: "CompetenceId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetencePrerequis_PrerequisId",
                table: "CompetencePrerequis",
                column: "PrerequisId");

            migrationBuilder.CreateIndex(
                name: "IX_DomaineCompetence_CompetenceId",
                table: "DomaineCompetence",
                column: "CompetenceId");

            migrationBuilder.CreateIndex(
                name: "IX_DomaineCompetence_DomaineId",
                table: "DomaineCompetence",
                column: "DomaineId");

            migrationBuilder.CreateIndex(
                name: "IX_DomaineUser_ApplicationUserId",
                table: "DomaineUser",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DomaineUser_DomaineId",
                table: "DomaineUser",
                column: "DomaineId");

            migrationBuilder.CreateIndex(
                name: "IX_Note_ApplicationUserId",
                table: "Note",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Note_SuiviNiveauId",
                table: "Note",
                column: "SuiviNiveauId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientUser_ApplicationUserId",
                table: "PatientUser",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientUser_PatientId",
                table: "PatientUser",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_PrerequisNiveau_NiveauId",
                table: "PrerequisNiveau",
                column: "NiveauId");

            migrationBuilder.CreateIndex(
                name: "IX_PrerequisNiveau_PrerequisId",
                table: "PrerequisNiveau",
                column: "PrerequisId");

            migrationBuilder.CreateIndex(
                name: "IX_Suivi_DomaineId",
                table: "Suivi",
                column: "DomaineId");

            migrationBuilder.CreateIndex(
                name: "IX_Suivi_PatientId",
                table: "Suivi",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_SuiviApplicationUser_ApplicationUserId",
                table: "SuiviApplicationUser",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SuiviApplicationUser_SuiviId",
                table: "SuiviApplicationUser",
                column: "SuiviId");

            migrationBuilder.CreateIndex(
                name: "IX_SuiviCompetence_CompetenceId",
                table: "SuiviCompetence",
                column: "CompetenceId");

            migrationBuilder.CreateIndex(
                name: "IX_SuiviCompetence_SuiviId",
                table: "SuiviCompetence",
                column: "SuiviId");

            migrationBuilder.CreateIndex(
                name: "IX_SuiviExercice_SuiviNiveauId",
                table: "SuiviExercice",
                column: "SuiviNiveauId");

            migrationBuilder.CreateIndex(
                name: "IX_SuiviExercice_ValideurId",
                table: "SuiviExercice",
                column: "ValideurId");

            migrationBuilder.CreateIndex(
                name: "IX_SuiviNiveau_NiveauId",
                table: "SuiviNiveau",
                column: "NiveauId");

            migrationBuilder.CreateIndex(
                name: "IX_SuiviNiveau_SuiviPrerequisId",
                table: "SuiviNiveau",
                column: "SuiviPrerequisId");

            migrationBuilder.CreateIndex(
                name: "IX_SuiviPrerequis_PrerequisId",
                table: "SuiviPrerequis",
                column: "PrerequisId");

            migrationBuilder.CreateIndex(
                name: "IX_SuiviPrerequis_SuiviCompetenceId",
                table: "SuiviPrerequis",
                column: "SuiviCompetenceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CompetencePrerequis");

            migrationBuilder.DropTable(
                name: "DomaineCompetence");

            migrationBuilder.DropTable(
                name: "DomaineUser");

            migrationBuilder.DropTable(
                name: "Note");

            migrationBuilder.DropTable(
                name: "PatientUser");

            migrationBuilder.DropTable(
                name: "PrerequisNiveau");

            migrationBuilder.DropTable(
                name: "SuiviApplicationUser");

            migrationBuilder.DropTable(
                name: "SuiviExercice");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "SuiviNiveau");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Niveau");

            migrationBuilder.DropTable(
                name: "SuiviPrerequis");

            migrationBuilder.DropTable(
                name: "Prerequis");

            migrationBuilder.DropTable(
                name: "SuiviCompetence");

            migrationBuilder.DropTable(
                name: "Competence");

            migrationBuilder.DropTable(
                name: "Suivi");

            migrationBuilder.DropTable(
                name: "Domaine");

            migrationBuilder.DropTable(
                name: "Patient");
        }
    }
}
