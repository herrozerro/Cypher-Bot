using Microsoft.EntityFrameworkCore.Migrations;

namespace CypherBot.Core.DataAccess.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    CharacterId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Player = table.Column<string>(maxLength: 30, nullable: true),
                    Name = table.Column<string>(maxLength: 30, nullable: true),
                    Tier = table.Column<int>(nullable: false),
                    XP = table.Column<int>(nullable: false),
                    MightPool = table.Column<int>(nullable: false),
                    SpeedPool = table.Column<int>(nullable: false),
                    IntPool = table.Column<int>(nullable: false),
                    RecoveryDie = table.Column<int>(nullable: false),
                    RecoveryMod = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.CharacterId);
                });

            migrationBuilder.CreateTable(
                name: "Cyphers",
                columns: table => new
                {
                    CypherId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 30, nullable: true),
                    Type = table.Column<string>(maxLength: 15, nullable: true),
                    LevelDie = table.Column<int>(nullable: false),
                    LevelBonus = table.Column<int>(nullable: false),
                    Effect = table.Column<string>(type: "varchar(2000)", nullable: true),
                    Source = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cyphers", x => x.CypherId);
                });

            migrationBuilder.CreateTable(
                name: "CharacterCyphers",
                columns: table => new
                {
                    CypherId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CharacterId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 30, nullable: true),
                    Type = table.Column<string>(maxLength: 15, nullable: true),
                    Level = table.Column<int>(nullable: false),
                    LevelDie = table.Column<int>(nullable: false),
                    LevelBonus = table.Column<int>(nullable: false),
                    Effect = table.Column<string>(type: "varchar(2000)", nullable: true),
                    Source = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterCyphers", x => x.CypherId);
                    table.ForeignKey(
                        name: "FK_CharacterCyphers_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacterInventories",
                columns: table => new
                {
                    InventoryId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CharacterId = table.Column<int>(nullable: false),
                    ItemName = table.Column<string>(maxLength: 50, nullable: true),
                    Qty = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterInventories", x => x.InventoryId);
                    table.ForeignKey(
                        name: "FK_CharacterInventories_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacterRecoveryRolls",
                columns: table => new
                {
                    RecoveryRollId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CharacterId = table.Column<int>(nullable: false),
                    RollName = table.Column<string>(maxLength: 25, nullable: true),
                    IsUsed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterRecoveryRolls", x => x.RecoveryRollId);
                    table.ForeignKey(
                        name: "FK_CharacterRecoveryRolls_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterCyphers_CharacterId",
                table: "CharacterCyphers",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterInventories_CharacterId",
                table: "CharacterInventories",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterRecoveryRolls_CharacterId",
                table: "CharacterRecoveryRolls",
                column: "CharacterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterCyphers");

            migrationBuilder.DropTable(
                name: "CharacterInventories");

            migrationBuilder.DropTable(
                name: "CharacterRecoveryRolls");

            migrationBuilder.DropTable(
                name: "Cyphers");

            migrationBuilder.DropTable(
                name: "Characters");
        }
    }
}
