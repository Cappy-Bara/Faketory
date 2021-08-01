using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Faketory.Infrastructure.Migrations
{
    public partial class DbInitialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlcModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CpuModel = table.Column<int>(type: "int", nullable: false),
                    Cpu = table.Column<int>(type: "int", nullable: false),
                    Rack = table.Column<short>(type: "smallint", nullable: false),
                    Slot = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlcModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Slots",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlcId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slots", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Email);
                });

            migrationBuilder.CreateTable(
                name: "InputsOutputs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Bit = table.Column<int>(type: "int", nullable: false),
                    Byte = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<bool>(type: "bit", nullable: false),
                    SlotId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InputsOutputs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InputsOutputs_Slots_SlotId",
                        column: x => x.SlotId,
                        principalTable: "Slots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Plcs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SlotId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plcs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Plcs_PlcModels_ModelId",
                        column: x => x.ModelId,
                        principalTable: "PlcModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Plcs_Slots_SlotId",
                        column: x => x.SlotId,
                        principalTable: "Slots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "PlcModels",
                columns: new[] { "Id", "Cpu", "CpuModel", "Rack", "Slot" },
                values: new object[,]
                {
                    { 1200, 30, 1200, (short)0, (short)1 },
                    { 1500, 40, 1500, (short)0, (short)1 },
                    { 300, 10, 300, (short)0, (short)2 },
                    { 400, 20, 400, (short)0, (short)2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_InputsOutputs_SlotId",
                table: "InputsOutputs",
                column: "SlotId");

            migrationBuilder.CreateIndex(
                name: "IX_Plcs_ModelId",
                table: "Plcs",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Plcs_SlotId",
                table: "Plcs",
                column: "SlotId",
                unique: true,
                filter: "[SlotId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InputsOutputs");

            migrationBuilder.DropTable(
                name: "Plcs");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "PlcModels");

            migrationBuilder.DropTable(
                name: "Slots");
        }
    }
}
