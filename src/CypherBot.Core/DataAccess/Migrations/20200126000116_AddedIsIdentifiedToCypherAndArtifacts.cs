using Microsoft.EntityFrameworkCore.Migrations;

namespace CypherBot.Core.DataAccess.Migrations
{
    public partial class AddedIsIdentifiedToCypherAndArtifacts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsIdentified",
                table: "CharacterCyphers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsIdentified",
                table: "CharacterArtifacts",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsIdentified",
                table: "CharacterCyphers");

            migrationBuilder.DropColumn(
                name: "IsIdentified",
                table: "CharacterArtifacts");
        }
    }
}
