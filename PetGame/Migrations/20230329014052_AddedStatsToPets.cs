using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetGame.Migrations
{
    /// <inheritdoc />
    public partial class AddedStatsToPets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Dexterity",
                table: "Pets",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SkillPointsEarned",
                table: "Pets",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SkillPointsSpent",
                table: "Pets",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Stamina",
                table: "Pets",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Strength",
                table: "Pets",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dexterity",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "SkillPointsEarned",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "SkillPointsSpent",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "Stamina",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "Strength",
                table: "Pets");
        }
    }
}
