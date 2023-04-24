using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetGame.Migrations
{
    /// <inheritdoc />
    public partial class AddingAchievements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlayerAchievements",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlayerId = table.Column<long>(type: "INTEGER", nullable: false),
                    Achievement = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerAchievements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerAchievements_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerStats",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlayerId = table.Column<long>(type: "INTEGER", nullable: false),
                    Stat = table.Column<int>(type: "INTEGER", nullable: false),
                    Value = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerStats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerStats_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerAchievements_PlayerId",
                table: "PlayerAchievements",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerStats_PlayerId",
                table: "PlayerStats",
                column: "PlayerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerAchievements");

            migrationBuilder.DropTable(
                name: "PlayerStats");
        }
    }
}
