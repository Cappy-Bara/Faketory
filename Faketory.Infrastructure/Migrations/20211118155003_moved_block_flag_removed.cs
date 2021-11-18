using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Faketory.Infrastructure.Migrations
{
    public partial class moved_block_flag_removed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConveyingPoints");

            migrationBuilder.DropColumn(
                name: "MovementFinished",
                table: "Pallets");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "MovementFinished",
                table: "Pallets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ConveyingPoints",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConveyorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Delay = table.Column<bool>(type: "bit", nullable: false),
                    LastPoint = table.Column<bool>(type: "bit", nullable: false),
                    PosX = table.Column<int>(type: "int", nullable: false),
                    PosY = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConveyingPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConveyingPoints_Conveyors_ConveyorId",
                        column: x => x.ConveyorId,
                        principalTable: "Conveyors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConveyingPoints_ConveyorId",
                table: "ConveyingPoints",
                column: "ConveyorId");
        }
    }
}
