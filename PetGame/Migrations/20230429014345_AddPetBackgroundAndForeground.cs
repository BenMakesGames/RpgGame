using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetGame.Migrations
{
    /// <inheritdoc />
    public partial class AddPetBackgroundAndForeground : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BackgroundImage",
                table: "Pets",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ForegroundImage",
                table: "Pets",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BackgroundImage",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "ForegroundImage",
                table: "Pets");
        }
    }
}
