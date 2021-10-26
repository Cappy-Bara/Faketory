using Microsoft.EntityFrameworkCore.Migrations;

namespace Faketory.Infrastructure.Migrations
{
    public partial class ioondeletecascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InputsOutputs_Slots_SlotId",
                table: "InputsOutputs");

            migrationBuilder.AddForeignKey(
                name: "FK_InputsOutputs_Slots_SlotId",
                table: "InputsOutputs",
                column: "SlotId",
                principalTable: "Slots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InputsOutputs_Slots_SlotId",
                table: "InputsOutputs");

            migrationBuilder.AddForeignKey(
                name: "FK_InputsOutputs_Slots_SlotId",
                table: "InputsOutputs",
                column: "SlotId",
                principalTable: "Slots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
