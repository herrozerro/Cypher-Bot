﻿// <auto-generated />
using CypherBot.Core.DataAccess.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CypherBot.Core.dataaccess.Migrations
{
    [DbContext(typeof(CypherContext))]
    partial class CypherContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("CypherBot.Core.Models.Artifact", b =>
                {
                    b.Property<int>("ArtifactId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Depletion")
                        .HasColumnType("text");

                    b.Property<string>("Effect")
                        .HasColumnType("text");

                    b.Property<string>("Form")
                        .HasColumnType("text");

                    b.Property<string>("Genre")
                        .HasColumnType("text");

                    b.Property<int>("LevelBonus")
                        .HasColumnType("integer");

                    b.Property<int>("LevelDie")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Source")
                        .HasColumnType("text");

                    b.HasKey("ArtifactId");

                    b.ToTable("Artifacts");
                });

            modelBuilder.Entity("CypherBot.Core.Models.ArtifactQuirk", b =>
                {
                    b.Property<int>("ArtifactQuirkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("EndRange")
                        .HasColumnType("integer");

                    b.Property<string>("Quirk")
                        .HasColumnType("text");

                    b.Property<int>("StartRange")
                        .HasColumnType("integer");

                    b.HasKey("ArtifactQuirkId");

                    b.ToTable("ArtifactQuirks");
                });

            modelBuilder.Entity("CypherBot.Core.Models.Character", b =>
                {
                    b.Property<int>("CharacterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Descriptor")
                        .HasColumnType("text");

                    b.Property<int>("Effort")
                        .HasColumnType("integer");

                    b.Property<string>("Focus")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Player")
                        .HasColumnType("text");

                    b.Property<int>("RecoveryDie")
                        .HasColumnType("integer");

                    b.Property<int>("RecoveryMod")
                        .HasColumnType("integer");

                    b.Property<int>("Tier")
                        .HasColumnType("integer");

                    b.Property<string>("Type")
                        .HasColumnType("text");

                    b.Property<int>("XP")
                        .HasColumnType("integer");

                    b.HasKey("CharacterId");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("CypherBot.Core.Models.CharacterAbility", b =>
                {
                    b.Property<int>("CharacterAbilityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("CharacterId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("Tier")
                        .HasColumnType("integer");

                    b.HasKey("CharacterAbilityId");

                    b.HasIndex("CharacterId");

                    b.ToTable("CharacterAbilities");
                });

            modelBuilder.Entity("CypherBot.Core.Models.CharacterArtifact", b =>
                {
                    b.Property<int>("ArtifactId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("CharacterId")
                        .HasColumnType("integer");

                    b.Property<string>("Depletion")
                        .HasColumnType("text");

                    b.Property<string>("Effect")
                        .HasColumnType("text");

                    b.Property<string>("Form")
                        .HasColumnType("text");

                    b.Property<string>("Genre")
                        .HasColumnType("text");

                    b.Property<bool>("IsIdentified")
                        .HasColumnType("boolean");

                    b.Property<int>("Level")
                        .HasColumnType("integer");

                    b.Property<int>("LevelBonus")
                        .HasColumnType("integer");

                    b.Property<int>("LevelDie")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Quirk")
                        .HasColumnType("text");

                    b.Property<string>("Source")
                        .HasColumnType("text");

                    b.HasKey("ArtifactId");

                    b.HasIndex("CharacterId");

                    b.ToTable("CharacterArtifacts");
                });

            modelBuilder.Entity("CypherBot.Core.Models.CharacterCypher", b =>
                {
                    b.Property<int>("CypherId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("CharacterId")
                        .HasColumnType("integer");

                    b.Property<string>("Effect")
                        .HasColumnType("text");

                    b.Property<string>("EffectOption")
                        .HasColumnType("text");

                    b.Property<string>("Form")
                        .HasColumnType("text");

                    b.Property<bool>("IsIdentified")
                        .HasColumnType("boolean");

                    b.Property<int>("Level")
                        .HasColumnType("integer");

                    b.Property<int>("LevelBonus")
                        .HasColumnType("integer");

                    b.Property<int>("LevelDie")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Source")
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .HasColumnType("text");

                    b.HasKey("CypherId");

                    b.HasIndex("CharacterId");

                    b.ToTable("CharacterCyphers");
                });

            modelBuilder.Entity("CypherBot.Core.Models.CharacterInventory", b =>
                {
                    b.Property<int>("InventoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("CharacterId")
                        .HasColumnType("integer");

                    b.Property<string>("ItemName")
                        .HasColumnType("text");

                    b.Property<int>("Qty")
                        .HasColumnType("integer");

                    b.HasKey("InventoryId");

                    b.HasIndex("CharacterId");

                    b.ToTable("CharacterInventories");
                });

            modelBuilder.Entity("CypherBot.Core.Models.CharacterPool", b =>
                {
                    b.Property<int>("PoolId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("CharacterId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("PoolCurrentVaue")
                        .HasColumnType("integer");

                    b.Property<int>("PoolEdge")
                        .HasColumnType("integer");

                    b.Property<int>("PoolIndex")
                        .HasColumnType("integer");

                    b.Property<int>("PoolMax")
                        .HasColumnType("integer");

                    b.HasKey("PoolId");

                    b.HasIndex("CharacterId");

                    b.ToTable("CharacterPools");
                });

            modelBuilder.Entity("CypherBot.Core.Models.CharacterRecoveryRoll", b =>
                {
                    b.Property<int>("RecoveryRollId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("CharacterId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsUsed")
                        .HasColumnType("boolean");

                    b.Property<string>("RollName")
                        .HasColumnType("text");

                    b.HasKey("RecoveryRollId");

                    b.HasIndex("CharacterId");

                    b.ToTable("CharacterRecoveryRolls");
                });

            modelBuilder.Entity("CypherBot.Core.Models.Creature", b =>
                {
                    b.Property<int>("CreatureId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Armor")
                        .HasColumnType("integer");

                    b.Property<string>("Combat")
                        .HasColumnType("text");

                    b.Property<int>("DamageInflicted")
                        .HasColumnType("integer");

                    b.Property<string>("Environment")
                        .HasColumnType("text");

                    b.Property<string>("GMIntrusions")
                        .HasColumnType("text");

                    b.Property<int>("Health")
                        .HasColumnType("integer");

                    b.Property<string>("Interaction")
                        .HasColumnType("text");

                    b.Property<int>("Level")
                        .HasColumnType("integer");

                    b.Property<string>("Modifications")
                        .HasColumnType("text");

                    b.Property<string>("Motive")
                        .HasColumnType("text");

                    b.Property<string>("Movement")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Use")
                        .HasColumnType("text");

                    b.HasKey("CreatureId");

                    b.ToTable("Creatures");
                });

            modelBuilder.Entity("CypherBot.Core.Models.Cypher", b =>
                {
                    b.Property<int>("CypherId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Effect")
                        .HasColumnType("text");

                    b.Property<int>("LevelBonus")
                        .HasColumnType("integer");

                    b.Property<int>("LevelDie")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Source")
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .HasColumnType("text");

                    b.HasKey("CypherId");

                    b.ToTable("Cyphers");
                });

            modelBuilder.Entity("CypherBot.Core.Models.CypherEffectOption", b =>
                {
                    b.Property<int>("EffectOptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("CypherId")
                        .HasColumnType("integer");

                    b.Property<string>("EffectDescription")
                        .HasColumnType("text");

                    b.Property<int>("EndRange")
                        .HasColumnType("integer");

                    b.Property<int>("StartRange")
                        .HasColumnType("integer");

                    b.HasKey("EffectOptionId");

                    b.HasIndex("CypherId");

                    b.ToTable("CypherEffectOptions");
                });

            modelBuilder.Entity("CypherBot.Core.Models.CypherFormOption", b =>
                {
                    b.Property<int>("FormOptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("CypherId")
                        .HasColumnType("integer");

                    b.Property<string>("Form")
                        .HasColumnType("text");

                    b.Property<string>("FormDescription")
                        .HasColumnType("text");

                    b.HasKey("FormOptionId");

                    b.HasIndex("CypherId");

                    b.ToTable("CypherFormOptions");
                });

            modelBuilder.Entity("CypherBot.Core.Models.Oddity", b =>
                {
                    b.Property<int>("OddityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("OddityDescription")
                        .HasColumnType("text");

                    b.HasKey("OddityId");

                    b.ToTable("Oddities");
                });

            modelBuilder.Entity("CypherBot.Core.Models.UnidentifiedArtifact", b =>
                {
                    b.Property<int>("UnidentifiedArtifactId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Depletion")
                        .HasColumnType("text");

                    b.Property<string>("Effect")
                        .HasColumnType("text");

                    b.Property<string>("Form")
                        .HasColumnType("text");

                    b.Property<string>("Genre")
                        .HasColumnType("text");

                    b.Property<bool>("IsIdentified")
                        .HasColumnType("boolean");

                    b.Property<int>("Level")
                        .HasColumnType("integer");

                    b.Property<int>("LevelBonus")
                        .HasColumnType("integer");

                    b.Property<int>("LevelDie")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Quirk")
                        .HasColumnType("text");

                    b.Property<string>("Source")
                        .HasColumnType("text");

                    b.Property<string>("UnidentifiedArtifactKey")
                        .HasColumnType("text");

                    b.HasKey("UnidentifiedArtifactId");

                    b.ToTable("UnidentifiedArtifacts");
                });

            modelBuilder.Entity("CypherBot.Core.Models.UnidentifiedCypher", b =>
                {
                    b.Property<int>("UnidentifiedCypherId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Effect")
                        .HasColumnType("text");

                    b.Property<string>("EffectOption")
                        .HasColumnType("text");

                    b.Property<string>("Form")
                        .HasColumnType("text");

                    b.Property<bool>("IsIdentified")
                        .HasColumnType("boolean");

                    b.Property<int>("Level")
                        .HasColumnType("integer");

                    b.Property<int>("LevelBonus")
                        .HasColumnType("integer");

                    b.Property<int>("LevelDie")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Source")
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .HasColumnType("text");

                    b.Property<string>("UnidentifiedCypherKey")
                        .HasColumnType("text");

                    b.HasKey("UnidentifiedCypherId");

                    b.ToTable("UnidentifiedCyphers");
                });

            modelBuilder.Entity("CypherBot.Core.Models.CharacterAbility", b =>
                {
                    b.HasOne("CypherBot.Core.Models.Character", "Character")
                        .WithMany("Abilities")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CypherBot.Core.Models.CharacterArtifact", b =>
                {
                    b.HasOne("CypherBot.Core.Models.Character", "Character")
                        .WithMany("Artifacts")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CypherBot.Core.Models.CharacterCypher", b =>
                {
                    b.HasOne("CypherBot.Core.Models.Character", "Character")
                        .WithMany("Cyphers")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CypherBot.Core.Models.CharacterInventory", b =>
                {
                    b.HasOne("CypherBot.Core.Models.Character", "Character")
                        .WithMany("Inventory")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CypherBot.Core.Models.CharacterPool", b =>
                {
                    b.HasOne("CypherBot.Core.Models.Character", "Character")
                        .WithMany("Pools")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CypherBot.Core.Models.CharacterRecoveryRoll", b =>
                {
                    b.HasOne("CypherBot.Core.Models.Character", "Character")
                        .WithMany("RecoveryRolls")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CypherBot.Core.Models.CypherEffectOption", b =>
                {
                    b.HasOne("CypherBot.Core.Models.Cypher", "Cypher")
                        .WithMany("EffectOptions")
                        .HasForeignKey("CypherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CypherBot.Core.Models.CypherFormOption", b =>
                {
                    b.HasOne("CypherBot.Core.Models.Cypher", "Cypher")
                        .WithMany("Forms")
                        .HasForeignKey("CypherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
