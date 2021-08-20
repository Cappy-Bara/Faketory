using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Faketory.Infrastructure.Migrations
{
    public partial class slot_from_plc_removed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plcs_Slots_SlotId",
                table: "Plcs");

            migrationBuilder.DropIndex(
                name: "IX_Plcs_SlotId",
                table: "Plcs");

            migrationBuilder.DropColumn(
                name: "SlotId",
                table: "Plcs");

            migrationBuilder.CreateIndex(
                name: "IX_Slots_PlcId",
                table: "Slots",
                column: "PlcId",
                unique: true,
                filter: "[PlcId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Slots_Plcs_PlcId",
                table: "Slots",
                column: "PlcId",
                principalTable: "Plcs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slots_Plcs_PlcId",
                table: "Slots");

            migrationBuilder.DropIndex(
                name: "IX_Slots_PlcId",
                table: "Slots");

            migrationBuilder.AddColumn<Guid>(
                name: "SlotId",
                table: "Plcs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Plcs_SlotId",
                table: "Plcs",
                column: "SlotId",
                unique: true,
                filter: "[SlotId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Plcs_Slots_SlotId",
                table: "Plcs",
                column: "SlotId",
                principalTable: "Slots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
