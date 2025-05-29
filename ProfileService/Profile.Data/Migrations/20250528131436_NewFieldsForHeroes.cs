using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Profile.Data.Migrations
{
    /// <inheritdoc />
    public partial class NewFieldsForHeroes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "cards_amount",
                table: "profile_heroes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cards_amount",
                table: "profile_heroes");
        }
    }
}
