using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kolosok.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedAction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Action",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ActionTime = table.Column<string>(type: "text", nullable: false),
                    ActionType = table.Column<string>(type: "text", nullable: false),
                    ActionPlace = table.Column<string>(type: "text", nullable: false),
                    BrigadeRescuerId = table.Column<Guid>(type: "uuid", nullable: false),
                    VictimId = table.Column<Guid>(type: "uuid", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Action", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Action_BrigadeRescuers_BrigadeRescuerId",
                        column: x => x.BrigadeRescuerId,
                        principalTable: "BrigadeRescuers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Action_Victims_VictimId",
                        column: x => x.VictimId,
                        principalTable: "Victims",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Action_BrigadeRescuerId",
                table: "Action",
                column: "BrigadeRescuerId");

            migrationBuilder.CreateIndex(
                name: "IX_Action_VictimId",
                table: "Action",
                column: "VictimId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Action");
        }
    }
}
