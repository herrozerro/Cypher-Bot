using Microsoft.EntityFrameworkCore.Migrations;

namespace CypherBot.Core.DataAccess.Migrations
{
    public partial class AddedArtifactQuirks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Quirk",
                table: "CharacterArtifacts",
                maxLength: 500,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ArtifactQuirks",
                columns: table => new
                {
                    ArtifactQuirkId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StartRange = table.Column<int>(nullable: false),
                    EndRange = table.Column<int>(nullable: false),
                    Quirk = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtifactQuirks", x => x.ArtifactQuirkId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtifactQuirks");

            migrationBuilder.DropColumn(
                name: "Quirk",
                table: "CharacterArtifacts");
        }
    }
}
