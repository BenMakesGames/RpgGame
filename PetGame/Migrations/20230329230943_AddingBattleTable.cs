using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetGame.Migrations
{
    /// <inheritdoc />
    public partial class AddingBattleTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Battles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlayerId = table.Column<long>(type: "INTEGER", nullable: false),
                    PetId = table.Column<long>(type: "INTEGER", nullable: false),
                    MonsterName = table.Column<string>(type: "TEXT", nullable: false),
                    MonsterMaxHealth = table.Column<int>(type: "INTEGER", nullable: false),
                    MonsterHealth = table.Column<int>(type: "INTEGER", nullable: false),
                    MonsterAttackDice = table.Column<int>(type: "INTEGER", nullable: false),
                    MonsterAttackDiceSides = table.Column<int>(type: "INTEGER", nullable: false),
                    MonsterAttackBonus = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Battles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Battles_Pets_PetId",
                        column: x => x.PetId,
                        principalTable: "Pets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Battles_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Battles_PetId",
                table: "Battles",
                column: "PetId");

            migrationBuilder.CreateIndex(
                name: "IX_Battles_PlayerId",
                table: "Battles",
                column: "PlayerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Battles");
        }
    }
}
