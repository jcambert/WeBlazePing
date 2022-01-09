using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WePing.Gir.Migrations
{
    public partial class InitialModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ModeleRencontre",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Reference = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<string>(type: "text", nullable: true),
                    NombreMancheGagnante = table.Column<int>(type: "integer", nullable: false),
                    NombreDeGroupe = table.Column<int>(type: "integer", nullable: false),
                    ScoreAquis = table.Column<bool>(type: "boolean", nullable: false),
                    ParticipationUniqueAuDouble = table.Column<bool>(type: "boolean", nullable: false),
                    NombreRemplacant = table.Column<int>(type: "integer", nullable: false),
                    AutoriseRemplacement = table.Column<int>(type: "integer", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeleRencontre", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModeleRencontrePartie",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    JoueurA = table.Column<string>(type: "text", nullable: true),
                    JoueurB = table.Column<string>(type: "text", nullable: true),
                    EstDouble = table.Column<bool>(type: "boolean", nullable: false),
                    ModeleRencontreId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeleRencontrePartie", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModeleRencontrePartie_ModeleRencontre_ModeleRencontreId",
                        column: x => x.ModeleRencontreId,
                        principalTable: "ModeleRencontre",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Rencontre",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ModeleId = table.Column<Guid>(type: "uuid", nullable: true),
                    NombreDePartie = table.Column<int>(type: "integer", nullable: false),
                    ScoreAquis = table.Column<bool>(type: "boolean", nullable: false),
                    Locked = table.Column<bool>(type: "boolean", nullable: false),
                    LockOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LockBy = table.Column<string>(type: "text", nullable: true),
                    EquipeDomicile = table.Column<string>(type: "text", nullable: true),
                    EquipeAdverse = table.Column<string>(type: "text", nullable: true),
                    EstPrete = table.Column<bool>(type: "boolean", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rencontre", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rencontre_ModeleRencontre_ModeleId",
                        column: x => x.ModeleId,
                        principalTable: "ModeleRencontre",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Joueurs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Ordre = table.Column<string>(type: "text", nullable: true),
                    Absent = table.Column<bool>(type: "boolean", nullable: false),
                    Licence = table.Column<string>(type: "text", nullable: true),
                    NumeroClub = table.Column<string>(type: "text", nullable: true),
                    Nom = table.Column<string>(type: "text", nullable: true),
                    Prenom = table.Column<string>(type: "text", nullable: true),
                    Points = table.Column<double>(type: "double precision", nullable: false),
                    Capitaine = table.Column<bool>(type: "boolean", nullable: false),
                    EstRemplacant = table.Column<bool>(type: "boolean", nullable: false),
                    RencontreId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Joueurs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Joueurs_Rencontre_RencontreId",
                        column: x => x.RencontreId,
                        principalTable: "Rencontre",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Parties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Index = table.Column<int>(type: "integer", nullable: false),
                    JoueurA = table.Column<string>(type: "text", nullable: true),
                    JoueurB = table.Column<string>(type: "text", nullable: true),
                    NomJoueurA = table.Column<string>(type: "text", nullable: true),
                    NomJoueurB = table.Column<string>(type: "text", nullable: true),
                    EstDouble = table.Column<bool>(type: "boolean", nullable: false),
                    NombreMancheGagnante = table.Column<int>(type: "integer", nullable: false),
                    RencontreId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parties_Rencontre_RencontreId",
                        column: x => x.RencontreId,
                        principalTable: "Rencontre",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Score",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Valeur = table.Column<int>(type: "integer", nullable: true),
                    PartieId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Score", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Score_Parties_PartieId",
                        column: x => x.PartieId,
                        principalTable: "Parties",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Joueurs_RencontreId",
                table: "Joueurs",
                column: "RencontreId");

            migrationBuilder.CreateIndex(
                name: "IX_ModeleRencontrePartie_ModeleRencontreId",
                table: "ModeleRencontrePartie",
                column: "ModeleRencontreId");

            migrationBuilder.CreateIndex(
                name: "IX_Parties_RencontreId",
                table: "Parties",
                column: "RencontreId");

            migrationBuilder.CreateIndex(
                name: "IX_Rencontre_ModeleId",
                table: "Rencontre",
                column: "ModeleId");

            migrationBuilder.CreateIndex(
                name: "IX_Score_PartieId",
                table: "Score",
                column: "PartieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Joueurs");

            migrationBuilder.DropTable(
                name: "ModeleRencontrePartie");

            migrationBuilder.DropTable(
                name: "Score");

            migrationBuilder.DropTable(
                name: "Parties");

            migrationBuilder.DropTable(
                name: "Rencontre");

            migrationBuilder.DropTable(
                name: "ModeleRencontre");
        }
    }
}
