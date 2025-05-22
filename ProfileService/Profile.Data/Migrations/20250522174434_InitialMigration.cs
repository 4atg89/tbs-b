using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Profile.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "profiles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    nickname = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    rating = table.Column<int>(type: "int", nullable: false),
                    coins = table.Column<int>(type: "int", nullable: false),
                    gems = table.Column<int>(type: "int", nullable: false),
                    clan_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    wins_count = table.Column<int>(type: "int", nullable: false),
                    max_rating = table.Column<int>(type: "int", nullable: false),
                    epic_wins_count = table.Column<int>(type: "int", nullable: false),
                    win_streak_count = table.Column<int>(type: "int", nullable: false),
                    battle_count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_profiles", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "profile_heroes",
                columns: table => new
                {
                    profile_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    hero_id = table.Column<int>(type: "int", nullable: false),
                    level = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_profile_heroes", x => new { x.profile_id, x.hero_id });
                    table.ForeignKey(
                        name: "FK_profile_heroes_profiles_profile_id",
                        column: x => x.profile_id,
                        principalTable: "profiles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "profile_hand_heroes",
                columns: table => new
                {
                    profile_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    type = table.Column<int>(type: "int", nullable: false),
                    hero_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_profile_hand_heroes", x => new { x.profile_id, x.type, x.hero_id });
                    table.ForeignKey(
                        name: "FK_profile_hand_heroes_profile_heroes_profile_id_hero_id",
                        columns: x => new { x.profile_id, x.hero_id },
                        principalTable: "profile_heroes",
                        principalColumns: new[] { "profile_id", "hero_id" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_profile_hand_heroes_profiles_profile_id",
                        column: x => x.profile_id,
                        principalTable: "profiles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_profile_hand_heroes_profile_id_hero_id",
                table: "profile_hand_heroes",
                columns: new[] { "profile_id", "hero_id" });

            migrationBuilder.CreateIndex(
                name: "IX_profiles_clan_id",
                table: "profiles",
                column: "clan_id");

            migrationBuilder.CreateIndex(
                name: "IX_profiles_nickname",
                table: "profiles",
                column: "nickname",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "profile_hand_heroes");

            migrationBuilder.DropTable(
                name: "profile_heroes");

            migrationBuilder.DropTable(
                name: "profiles");
        }
    }
}
