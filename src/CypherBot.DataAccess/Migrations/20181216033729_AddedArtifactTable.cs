using Microsoft.EntityFrameworkCore.Migrations;

namespace CypherBot.DataAccess.Migrations
{
    public partial class AddedArtifactTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Artifacts",
                columns: table => new
                {
                    ArtifactId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 30, nullable: true),
                    Form = table.Column<string>(maxLength: 100, nullable: true),
                    LevelDie = table.Column<int>(nullable: false),
                    LevelBonus = table.Column<int>(nullable: false),
                    Effect = table.Column<string>(type: "varchar(2000)", maxLength: 1000, nullable: true),
                    Source = table.Column<string>(maxLength: 20, nullable: true),
                    Depletion = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artifacts", x => x.ArtifactId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Artifacts");
        }
    }
}
