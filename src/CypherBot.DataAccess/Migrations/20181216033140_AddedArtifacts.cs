using Microsoft.EntityFrameworkCore.Migrations;

namespace CypherBot.DataAccess.Migrations
{
    public partial class AddedArtifacts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CharacterArtifacts",
                columns: table => new
                {
                    ArtifactId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CharacterId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 30, nullable: true),
                    Form = table.Column<string>(maxLength: 100, nullable: true),
                    Level = table.Column<int>(nullable: false),
                    LevelDie = table.Column<int>(nullable: false),
                    LevelBonus = table.Column<int>(nullable: false),
                    Effect = table.Column<string>(type: "varchar(2000)", maxLength: 1000, nullable: true),
                    Source = table.Column<string>(maxLength: 20, nullable: true),
                    Depletion = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterArtifacts", x => x.ArtifactId);
                    table.ForeignKey(
                        name: "FK_CharacterArtifacts_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterArtifacts_CharacterId",
                table: "CharacterArtifacts",
                column: "CharacterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterArtifacts");
        }
    }
}
