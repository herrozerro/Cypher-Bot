using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CypherBot.Core.DataAccess.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArtifactQuirks",
                columns: table => new
                {
                    ArtifactQuirkId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StartRange = table.Column<int>(nullable: false),
                    EndRange = table.Column<int>(nullable: false),
                    Quirk = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtifactQuirks", x => x.ArtifactQuirkId);
                });

            migrationBuilder.CreateTable(
                name: "Artifacts",
                columns: table => new
                {
                    ArtifactId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Form = table.Column<string>(maxLength: 500, nullable: true),
                    Genre = table.Column<string>(maxLength: 100, nullable: true),
                    LevelDie = table.Column<int>(nullable: false),
                    LevelBonus = table.Column<int>(nullable: false),
                    Effect = table.Column<string>(maxLength: 1000, nullable: true),
                    Source = table.Column<string>(maxLength: 100, nullable: true),
                    Depletion = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artifacts", x => x.ArtifactId);
                });

            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    CharacterId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Player = table.Column<string>(maxLength: 100, nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Tier = table.Column<int>(nullable: false),
                    XP = table.Column<int>(nullable: false),
                    Descriptor = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Focus = table.Column<string>(nullable: true),
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
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Type = table.Column<string>(maxLength: 100, nullable: true),
                    LevelDie = table.Column<int>(nullable: false),
                    LevelBonus = table.Column<int>(nullable: false),
                    Effect = table.Column<string>(nullable: true),
                    Source = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cyphers", x => x.CypherId);
                });

            migrationBuilder.CreateTable(
                name: "Descriptors",
                columns: table => new
                {
                    DescriptorId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
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
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foci", x => x.FocusId);
                });

            migrationBuilder.CreateTable(
                name: "Oddities",
                columns: table => new
                {
                    OddityId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OddityDescription = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oddities", x => x.OddityId);
                });

            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    TypeId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
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
                name: "UnidentifiedArtifacts",
                columns: table => new
                {
                    UnidentifiedArtifactId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UnidentifiedArtifactKey = table.Column<string>(type: "varchar(10)", nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Form = table.Column<string>(maxLength: 100, nullable: true),
                    Genre = table.Column<string>(maxLength: 100, nullable: true),
                    Quirk = table.Column<string>(nullable: true),
                    IsIdentified = table.Column<bool>(nullable: false),
                    LevelDie = table.Column<int>(nullable: false),
                    LevelBonus = table.Column<int>(nullable: false),
                    Effect = table.Column<string>(maxLength: 1000, nullable: true),
                    Source = table.Column<string>(maxLength: 100, nullable: true),
                    Depletion = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidentifiedArtifacts", x => x.UnidentifiedArtifactId);
                });

            migrationBuilder.CreateTable(
                name: "UnidentifiedCyphers",
                columns: table => new
                {
                    UnidentifiedCypherId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UnidentifiedCypherKey = table.Column<string>(type: "varchar(10)", nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Type = table.Column<string>(maxLength: 100, nullable: true),
                    IsIdentified = table.Column<bool>(nullable: false),
                    LevelDie = table.Column<int>(nullable: false),
                    LevelBonus = table.Column<int>(nullable: false),
                    Form = table.Column<string>(nullable: true),
                    Effect = table.Column<string>(type: "varchar(2000)", nullable: true),
                    EffectOption = table.Column<string>(nullable: true),
                    Source = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidentifiedCyphers", x => x.UnidentifiedCypherId);
                });

            migrationBuilder.CreateTable(
                name: "CharacterAbilities",
                columns: table => new
                {
                    CharacterAbilityId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CharacterId = table.Column<int>(nullable: false),
                    Tier = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
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
                name: "CharacterArtifacts",
                columns: table => new
                {
                    ArtifactId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CharacterId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Form = table.Column<string>(maxLength: 100, nullable: true),
                    Genre = table.Column<string>(nullable: true),
                    Quirk = table.Column<string>(maxLength: 1000, nullable: true),
                    IsIdentified = table.Column<bool>(nullable: false),
                    Level = table.Column<int>(nullable: false),
                    LevelDie = table.Column<int>(nullable: false),
                    LevelBonus = table.Column<int>(nullable: false),
                    Effect = table.Column<string>(maxLength: 1000, nullable: true),
                    Source = table.Column<string>(maxLength: 100, nullable: true),
                    Depletion = table.Column<string>(maxLength: 100, nullable: true)
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

            migrationBuilder.CreateTable(
                name: "CharacterCyphers",
                columns: table => new
                {
                    CypherId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CharacterId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Type = table.Column<string>(maxLength: 100, nullable: true),
                    IsIdentified = table.Column<bool>(nullable: false),
                    Level = table.Column<int>(nullable: false),
                    LevelDie = table.Column<int>(nullable: false),
                    LevelBonus = table.Column<int>(nullable: false),
                    Form = table.Column<string>(maxLength: 500, nullable: true),
                    Effect = table.Column<string>(maxLength: 1000, nullable: true),
                    EffectOption = table.Column<string>(maxLength: 500, nullable: true),
                    Source = table.Column<string>(maxLength: 100, nullable: true)
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
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CharacterId = table.Column<int>(nullable: false),
                    ItemName = table.Column<string>(maxLength: 500, nullable: true),
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
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CharacterId = table.Column<int>(nullable: false),
                    RollName = table.Column<string>(maxLength: 100, nullable: true),
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

            migrationBuilder.CreateTable(
                name: "CypherEffectOptions",
                columns: table => new
                {
                    EffectOptionId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CypherId = table.Column<int>(nullable: false),
                    StartRange = table.Column<int>(nullable: false),
                    EndRange = table.Column<int>(nullable: false),
                    EffectDescription = table.Column<string>(maxLength: 1000, nullable: true)
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
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CypherId = table.Column<int>(nullable: false),
                    Form = table.Column<string>(maxLength: 100, nullable: true),
                    FormDescription = table.Column<string>(maxLength: 1000, nullable: true)
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

            migrationBuilder.CreateTable(
                name: "DescriptorAbilities",
                columns: table => new
                {
                    DescriptorAbilityId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DescriptorId = table.Column<int>(nullable: false),
                    Tier = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    Source = table.Column<string>(maxLength: 100, nullable: true)
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
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FocusId = table.Column<int>(nullable: false),
                    Tier = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    Source = table.Column<string>(maxLength: 100, nullable: true)
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
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TypeId = table.Column<int>(nullable: false),
                    Tier = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    Source = table.Column<string>(maxLength: 100, nullable: true)
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
                name: "IX_CharacterArtifacts_CharacterId",
                table: "CharacterArtifacts",
                column: "CharacterId");

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

            migrationBuilder.CreateIndex(
                name: "IX_CypherEffectOptions_CypherId",
                table: "CypherEffectOptions",
                column: "CypherId");

            migrationBuilder.CreateIndex(
                name: "IX_CypherFormOptions_CypherId",
                table: "CypherFormOptions",
                column: "CypherId");

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
                name: "ArtifactQuirks");

            migrationBuilder.DropTable(
                name: "Artifacts");

            migrationBuilder.DropTable(
                name: "CharacterAbilities");

            migrationBuilder.DropTable(
                name: "CharacterArtifacts");

            migrationBuilder.DropTable(
                name: "CharacterCyphers");

            migrationBuilder.DropTable(
                name: "CharacterInventories");

            migrationBuilder.DropTable(
                name: "CharacterRecoveryRolls");

            migrationBuilder.DropTable(
                name: "CypherEffectOptions");

            migrationBuilder.DropTable(
                name: "CypherFormOptions");

            migrationBuilder.DropTable(
                name: "DescriptorAbilities");

            migrationBuilder.DropTable(
                name: "FociAbilities");

            migrationBuilder.DropTable(
                name: "Oddities");

            migrationBuilder.DropTable(
                name: "TypeAbilities");

            migrationBuilder.DropTable(
                name: "UnidentifiedArtifacts");

            migrationBuilder.DropTable(
                name: "UnidentifiedCyphers");

            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropTable(
                name: "Cyphers");

            migrationBuilder.DropTable(
                name: "Descriptors");

            migrationBuilder.DropTable(
                name: "Foci");

            migrationBuilder.DropTable(
                name: "Types");
        }
    }
}
