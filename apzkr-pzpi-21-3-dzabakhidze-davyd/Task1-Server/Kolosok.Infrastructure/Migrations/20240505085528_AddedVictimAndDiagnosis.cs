using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kolosok.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedVictimAndDiagnosis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Roles",
                table: "Contacts");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Contacts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Diagnoses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: false),
                    DetectionTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    VictimId = table.Column<Guid>(type: "uuid", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diagnoses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Diagnoses_Victims_VictimId",
                        column: x => x.VictimId,
                        principalTable: "Victims",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Diagnoses_VictimId",
                table: "Diagnoses",
                column: "VictimId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Diagnoses");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Contacts");

            migrationBuilder.AddColumn<int[]>(
                name: "Roles",
                table: "Contacts",
                type: "integer[]",
                nullable: true);
        }
    }
}
