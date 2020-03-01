using Microsoft.EntityFrameworkCore.Migrations;

namespace IrrigationServer.Migrations
{
    public partial class IrrigationServerIrrigationDbContextPiAzonosito : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Azonosito",
                table: "Pies",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Azonosito",
                table: "Pies",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
