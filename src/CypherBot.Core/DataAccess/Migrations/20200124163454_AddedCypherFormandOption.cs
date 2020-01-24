using Microsoft.EntityFrameworkCore.Migrations;

namespace CypherBot.Core.DataAccess.Migrations
{
    public partial class AddedCypherFormandOption : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EffectOption",
                table: "CharacterCyphers",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Form",
                table: "CharacterCyphers",
                maxLength: 500,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CypherEffectOptions",
                columns: table => new
                {
                    EffectOptionId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CypherId = table.Column<int>(nullable: false),
                    StartRange = table.Column<int>(nullable: false),
                    EndRange = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CypherEffectOptions", x => x.EffectOptionId);
                    table.ForeignKey(
                        name: "FK_CypherEffectOptions_Cyphers_CypherId",
                        column: x => x.CypherId,
                        principalTable: "Cyphers",
                        principalColumn: "CypherId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CypherFormOptions",
                columns: table => new
                {
                    FormOptionId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CypherId = table.Column<int>(nullable: false),
                    Form = table.Column<string>(maxLength: 50, nullable: true),
                    FormDescription = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CypherFormOptions", x => x.FormOptionId);
                    table.ForeignKey(
                        name: "FK_CypherFormOptions_Cyphers_CypherId",
                        column: x => x.CypherId,
                        principalTable: "Cyphers",
                        principalColumn: "CypherId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CypherEffectOptions_CypherId",
                table: "CypherEffectOptions",
                column: "CypherId");

            migrationBuilder.CreateIndex(
                name: "IX_CypherFormOptions_CypherId",
                table: "CypherFormOptions",
                column: "CypherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CypherEffectOptions");

            migrationBuilder.DropTable(
                name: "CypherFormOptions");

            migrationBuilder.DropColumn(
                name: "EffectOption",
                table: "CharacterCyphers");

            migrationBuilder.DropColumn(
                name: "Form",
                table: "CharacterCyphers");
        }
    }
}
