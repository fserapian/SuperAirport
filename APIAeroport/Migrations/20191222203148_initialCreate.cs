using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace APIAeroport.Migrations
{
    public partial class initialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aeroports",
                columns: table => new
                {
                    AeroportId = table.Column<string>(maxLength: 10, nullable: false),
                    NomAeroport = table.Column<string>(nullable: true),
                    Ville = table.Column<string>(nullable: true),
                    Pays = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aeroports", x => x.AeroportId);
                });

            migrationBuilder.CreateTable(
                name: "Compagnies",
                columns: table => new
                {
                    CompagnieId = table.Column<string>(maxLength: 10, nullable: false),
                    NomCompagnie = table.Column<string>(nullable: true),
                    logoUri = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compagnies", x => x.CompagnieId);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    VolCeduleId = table.Column<int>(nullable: false),
                    NumTel = table.Column<string>(maxLength: 20, nullable: false),
                    DateInscription = table.Column<DateTime>(nullable: false),
                    DateArret = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => new { x.VolCeduleId, x.NumTel });
                });

            migrationBuilder.CreateTable(
                name: "VolCedules",
                columns: table => new
                {
                    VolCeduleId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    VolGeneriqueId = table.Column<string>(nullable: true),
                    DatePrevue = table.Column<DateTime>(nullable: false),
                    DateRevisee = table.Column<DateTime>(nullable: false),
                    Statut = table.Column<int>(nullable: false),
                    Porte = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolCedules", x => x.VolCeduleId);
                });

            migrationBuilder.CreateTable(
                name: "VolGeneriques",
                columns: table => new
                {
                    VolGeneriqueId = table.Column<string>(maxLength: 10, nullable: false),
                    AeroportId = table.Column<string>(nullable: true),
                    CompagnieId = table.Column<string>(nullable: true),
                    HeurePrevue = table.Column<DateTime>(nullable: false),
                    Direction = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolGeneriques", x => x.VolGeneriqueId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aeroports");

            migrationBuilder.DropTable(
                name: "Compagnies");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "VolCedules");

            migrationBuilder.DropTable(
                name: "VolGeneriques");
        }
    }
}
