using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Faketory.Infrastructure.Migrations
{
    public partial class sensorsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sensor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PosX = table.Column<int>(type: "int", nullable: false),
                    PosY = table.Column<int>(type: "int", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IOId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsSensing = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sensor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sensor_InputsOutputs_IOId",
                        column: x => x.IOId,
                        principalTable: "InputsOutputs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sensor_IOId",
                table: "Sensor",
                column: "IOId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sensor");
        }
    }
}
