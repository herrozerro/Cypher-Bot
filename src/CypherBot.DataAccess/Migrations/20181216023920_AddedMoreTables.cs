using Microsoft.EntityFrameworkCore.Migrations;

namespace CypherBot.DataAccess.Migrations
{
    public partial class AddedMoreTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descriptor",
                table: "Characters",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Focus",
                table: "Characters",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Characters",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CharacterAbilities",
                columns: table => new
                {
                    CharacterAbilityId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CharacterId = table.Column<int>(nullable: false),
                    Tier = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 30, nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterAbilities", x => x.CharacterAbilityId);
                    table.ForeignKey(
                        name: "FK_CharacterAbilities_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Descriptors",
                columns: table => new
                {
                    DescriptorId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 30, nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Descriptors", x => x.DescriptorId);
                });

            migrationBuilder.CreateTable(
                name: "Foci",
                columns: table => new
                {
                    FocusId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 30, nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foci", x => x.FocusId);
                });

            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    TypeId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 30, nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    IntellectStartingPool = table.Column<int>(nullable: false),
                    MightStartingPool = table.Column<int>(nullable: false),
                    SpeedStartingPool = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types", x => x.TypeId);
                });

            migrationBuilder.CreateTable(
                name: "DescriptorAbilities",
                columns: table => new
                {
                    DescriptorAbilityId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DescriptorId = table.Column<int>(nullable: false),
                    Tier = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 30, nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    Source = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DescriptorAbilities", x => x.DescriptorAbilityId);
                    table.ForeignKey(
                        name: "FK_DescriptorAbilities_Descriptors_DescriptorId",
                        column: x => x.DescriptorId,
                        principalTable: "Descriptors",
                        principalColumn: "DescriptorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FociAbilities",
                columns: table => new
                {
                    FocusAbilityId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FocusId = table.Column<int>(nullable: false),
                    Tier = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 30, nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    Source = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FociAbilities", x => x.FocusAbilityId);
                    table.ForeignKey(
                        name: "FK_FociAbilities_Foci_FocusId",
                        column: x => x.FocusId,
                        principalTable: "Foci",
                        principalColumn: "FocusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TypeAbilities",
                columns: table => new
                {
                    TypeAbilityId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TypeId = table.Column<int>(nullable: false),
                    Tier = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 30, nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    Source = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeAbilities", x => x.TypeAbilityId);
                    table.ForeignKey(
                        name: "FK_TypeAbilities_Types_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Types",
                        principalColumn: "TypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterAbilities_CharacterId",
                table: "CharacterAbilities",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_DescriptorAbilities_DescriptorId",
                table: "DescriptorAbilities",
                column: "DescriptorId");

            migrationBuilder.CreateIndex(
                name: "IX_FociAbilities_FocusId",
                table: "FociAbilities",
                column: "FocusId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeAbilities_TypeId",
                table: "TypeAbilities",
                column: "TypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterAbilities");

            migrationBuilder.DropTable(
                name: "DescriptorAbilities");

            migrationBuilder.DropTable(
                name: "FociAbilities");

            migrationBuilder.DropTable(
                name: "TypeAbilities");

            migrationBuilder.DropTable(
                name: "Descriptors");

            migrationBuilder.DropTable(
                name: "Foci");

            migrationBuilder.DropTable(
                name: "Types");

            migrationBuilder.DropColumn(
                name: "Descriptor",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Focus",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Characters");
        }
    }
}
