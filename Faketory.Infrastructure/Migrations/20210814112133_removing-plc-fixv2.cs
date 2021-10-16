﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Faketory.Infrastructure.Migrations
{
    public partial class removingplcfixv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slots_Plcs_PlcId",
                table: "Slots");

            migrationBuilder.AddForeignKey(
                name: "FK_Slots_Plcs_PlcId",
                table: "Slots",
                column: "PlcId",
                principalTable: "Plcs",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slots_Plcs_PlcId",
                table: "Slots");

            migrationBuilder.AddForeignKey(
                name: "FK_Slots_Plcs_PlcId",
                table: "Slots",
                column: "PlcId",
                principalTable: "Plcs",
                principalColumn: "Id");
        }
    }
}