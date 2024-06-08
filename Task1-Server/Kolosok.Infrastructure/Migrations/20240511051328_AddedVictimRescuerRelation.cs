using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kolosok.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedVictimRescuerRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BrigadeSize",
                table: "Brigades");

            migrationBuilder.AddColumn<Guid>(
                name: "BrigadeRescuerId",
                table: "Victims",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Victims_BrigadeRescuerId",
                table: "Victims",
                column: "BrigadeRescuerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Victims_BrigadeRescuers_BrigadeRescuerId",
                table: "Victims",
                column: "BrigadeRescuerId",
                principalTable: "BrigadeRescuers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Victims_BrigadeRescuers_BrigadeRescuerId",
                table: "Victims");

            migrationBuilder.DropIndex(
                name: "IX_Victims_BrigadeRescuerId",
                table: "Victims");

            migrationBuilder.DropColumn(
                name: "BrigadeRescuerId",
                table: "Victims");

            migrationBuilder.AddColumn<int>(
                name: "BrigadeSize",
                table: "Brigades",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
