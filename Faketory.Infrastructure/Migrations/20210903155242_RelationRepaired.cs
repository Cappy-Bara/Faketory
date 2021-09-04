using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Faketory.Infrastructure.Migrations
{
    public partial class RelationRepaired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConveyingPoints_Pallets_ConveyorId",
                table: "ConveyingPoints");

            migrationBuilder.AddColumn<Guid>(
                name: "PalletToMoveId",
                table: "ConveyingPoints",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConveyingPoints_PalletToMoveId",
                table: "ConveyingPoints",
                column: "PalletToMoveId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConveyingPoints_Pallets_PalletToMoveId",
                table: "ConveyingPoints",
                column: "PalletToMoveId",
                principalTable: "Pallets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConveyingPoints_Pallets_PalletToMoveId",
                table: "ConveyingPoints");

            migrationBuilder.DropIndex(
                name: "IX_ConveyingPoints_PalletToMoveId",
                table: "ConveyingPoints");

            migrationBuilder.DropColumn(
                name: "PalletToMoveId",
                table: "ConveyingPoints");

            migrationBuilder.AddForeignKey(
                name: "FK_ConveyingPoints_Pallets_ConveyorId",
                table: "ConveyingPoints",
                column: "ConveyorId",
                principalTable: "Pallets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
