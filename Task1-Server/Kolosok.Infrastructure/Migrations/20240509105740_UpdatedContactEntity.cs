using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kolosok.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedContactEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Salt",
                table: "Contacts",
                type: "bytea",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salt",
                table: "Contacts");
        }
    }
}
