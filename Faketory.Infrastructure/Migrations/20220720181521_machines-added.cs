using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Faketory.Infrastructure.Migrations
{
    public partial class machinesadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Machines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PosX = table.Column<int>(type: "integer", nullable: false),
                    PosY = table.Column<int>(type: "integer", nullable: false),
                    UserEmail = table.Column<string>(type: "text", nullable: true),
                    LastProcessedPalletId = table.Column<Guid>(type: "uuid", nullable: false),
                    PalletAlreadyProcessed = table.Column<bool>(type: "boolean", nullable: false),
                    ProcessingTimestampAmount = table.Column<int>(type: "integer", nullable: false),
                    Ticks = table.Column<int>(type: "integer", nullable: false),
                    RandomFactor = table.Column<int>(type: "integer", nullable: false),
                    IsProcessing = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Machines", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Machines");
        }
    }
}
