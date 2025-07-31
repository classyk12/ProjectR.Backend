using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectR.Backend.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedURLandShortLinkToBusinessTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "businesses",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShortLink",
                table: "businesses",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Logo",
                table: "businesses");

            migrationBuilder.DropColumn(
                name: "ShortLink",
                table: "businesses");
        }
    }
}
