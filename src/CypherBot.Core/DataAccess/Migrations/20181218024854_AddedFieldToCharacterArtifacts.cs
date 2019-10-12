using Microsoft.EntityFrameworkCore.Migrations;

namespace CypherBot.Core.DataAccess.Migrations
{
    public partial class AddedFieldToCharacterArtifacts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "CharacterArtifacts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Genre",
                table: "CharacterArtifacts");
        }
    }
}
