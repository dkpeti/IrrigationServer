using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IrrigationServer.Migrations
{
    public partial class IrrigationServerIrrigationDbContextFirstContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Zonak",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nev = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zonak", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Szenzorok",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nev = table.Column<string>(nullable: true),
                    Tipus = table.Column<int>(nullable: false),
                    Megjegyzes = table.Column<string>(nullable: true),
                    ZonaId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Szenzorok", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Szenzorok_Zonak_ZonaId",
                        column: x => x.ZonaId,
                        principalTable: "Zonak",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Meresek",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mikor = table.Column<DateTime>(nullable: false),
                    MertAdat = table.Column<long>(nullable: false),
                    SzenzorId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meresek", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Meresek_Szenzorok_SzenzorId",
                        column: x => x.SzenzorId,
                        principalTable: "Szenzorok",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Meresek_SzenzorId",
                table: "Meresek",
                column: "SzenzorId");

            migrationBuilder.CreateIndex(
                name: "IX_Szenzorok_ZonaId",
                table: "Szenzorok",
                column: "ZonaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Meresek");

            migrationBuilder.DropTable(
                name: "Szenzorok");

            migrationBuilder.DropTable(
                name: "Zonak");
        }
    }
}
