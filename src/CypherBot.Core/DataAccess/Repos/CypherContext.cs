using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CypherBot.Core.Models;

namespace CypherBot.Core.DataAccess.Repos
{
    public class CypherContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=cs.db");
        }

        public DbSet<Character> Characters { get; set; }
        public DbSet<CharacterInventory> CharacterInventories { get; set; }
        public DbSet<CharacterRecoveryRoll> CharacterRecoveryRolls { get; set; }
        public DbSet<CharacterCypher> CharacterCyphers { get; set; }
        public DbSet<CharacterAbility> CharacterAbilities { get; set; }
        public DbSet<CharacterArtifact> CharacterArtifacts { get; set; }

        public DbSet<Cypher> Cyphers { get; set; }
        public DbSet<Artifact> Artifacts { get; set; }

        public DbSet<Models.Type> Types { get; set; }
        public DbSet<TypeAbility> TypeAbilities { get; set; }

        public DbSet<Descriptor> Descriptors { get; set; }
        public DbSet<DescriptorAbility> DescriptorAbilities { get; set; }

        public DbSet<Focus> Foci { get; set; }
        public DbSet<FocusAbility> FociAbilities { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Character>(e =>
            {
                e.HasKey(x => x.CharacterId);
                e.HasMany(x => x.Inventory);
                e.HasMany(x => x.RecoveryRolls);
                e.HasMany(x => x.Cyphers);
                e.HasMany(x => x.Abilities);
                e.HasMany(x => x.Artifacts);

                e.Property(x => x.CharacterId).ValueGeneratedOnAdd();

                e.Property(x => x.Name).HasMaxLength(30);
                e.Property(x => x.Player).HasMaxLength(30);
            });

            builder.Entity<CharacterCypher>(e =>
            {
                e.HasKey(x => x.CypherId);

                e.Property(x => x.CypherId).ValueGeneratedOnAdd();

                e.HasOne(x => x.Character)
                    .WithMany(x => x.Cyphers)
                    .HasForeignKey(x => x.CharacterId);

                e.Property(x => x.Effect)
                    .HasColumnType("varchar(2000)");

                e.Property(x => x.Name)
                    .HasMaxLength(30);

                e.Property(x => x.Source)
                    .HasMaxLength(20);

                e.Property(x => x.Type)
                    .HasMaxLength(15);

            });

            builder.Entity<CharacterRecoveryRoll>(e =>
            {
                e.HasKey(x => x.RecoveryRollId);

                e.HasOne(x => x.Character)
                    .WithMany(x => x.RecoveryRolls)
                    .HasForeignKey(x => x.CharacterId);

                e.Property(x => x.RollName)
                    .HasMaxLength(25);
            });

            builder.Entity<CharacterInventory>(e =>
            {
                e.HasKey(x => x.InventoryId);

                e.Property(x => x.InventoryId)
                    .ValueGeneratedOnAdd();
                    
                e.HasOne(x => x.Character)
                    .WithMany(x => x.Inventory)
                    .HasForeignKey(x => x.CharacterId);

                e.Property(x => x.ItemName)
                    .HasMaxLength(50);
            });

            builder.Entity<CharacterAbility>(e =>
            {
                e.HasKey(x => x.CharacterAbilityId);
                e.HasOne(x => x.Character)
                    .WithMany(x => x.Abilities)
                    .HasForeignKey(x => x.CharacterId);

                e.Property(x => x.Name)
                    .HasMaxLength(30);

                e.Property(x => x.Description)
                    .HasMaxLength(1000);
            });

            builder.Entity<CharacterArtifact>(e =>
            {
                e.HasKey(x => x.ArtifactId);

                e.Property(x => x.ArtifactId).ValueGeneratedOnAdd();

                e.HasOne(x => x.Character)
                    .WithMany(x => x.Artifacts)
                    .HasForeignKey(x => x.CharacterId);

                e.Property(x => x.Effect)
                    .HasColumnType("varchar(2000)");

                e.Property(x => x.Name)
                    .HasMaxLength(30);

                e.Property(x => x.Source)
                    .HasMaxLength(20);

                e.Property(x => x.Depletion)
                    .HasMaxLength(20);

                e.Property(x => x.Form)
                    .HasMaxLength(100);

                e.Property(x => x.Effect)
                    .HasMaxLength(1000);

            });

            builder.Entity<Cypher>(e =>
            {
                e.HasKey(x => x.CypherId);

                e.Property(x => x.CypherId).ValueGeneratedOnAdd();

                e.Property(x => x.Effect)
                    .HasColumnType("varchar(2000)");

                e.Property(x => x.Name)
                    .HasMaxLength(30);

                e.Property(x => x.Source)
                    .HasMaxLength(20);

                e.Property(x => x.Type)
                    .HasMaxLength(15);

                e.Ignore(x => x.Level);
            });

            builder.Entity<Artifact>(e =>
            {
                e.HasKey(x => x.ArtifactId);

                e.Property(x => x.ArtifactId).ValueGeneratedOnAdd();

                e.Property(x => x.Effect)
                    .HasColumnType("varchar(2000)");

                e.Property(x => x.Name)
                    .HasMaxLength(30);

                e.Property(x => x.Source)
                    .HasMaxLength(20);

                e.Property(x => x.Genre)
                    .HasMaxLength(20);

                e.Property(x => x.Depletion)
                    .HasMaxLength(20);

                e.Property(x => x.Form)
                    .HasMaxLength(100);

                e.Property(x => x.Effect)
                    .HasMaxLength(1000);

                e.Ignore(x => x.Level);

            });

            builder.Entity<Models.Type>(e =>
            {
                e.HasKey(x => x.TypeId);
                e.HasMany(x => x.TypeAbilities);

                e.Property(x => x.Name)
                    .HasMaxLength(30);

                e.Property(x => x.Description)
                    .HasMaxLength(1000);
            });

            builder.Entity<TypeAbility>(e =>
            {
                e.HasKey(x => x.TypeAbilityId);
                e.HasOne(x => x.Type)
                    .WithMany(x => x.TypeAbilities)
                    .HasForeignKey(x => x.TypeId);

                e.Property(x => x.Name)
                    .HasMaxLength(30);

                e.Property(x => x.Description)
                    .HasMaxLength(1000);

                e.Property(x => x.Source)
                    .HasMaxLength(20);
            });

            builder.Entity<Descriptor>(e =>
            {
                e.HasKey(x => x.DescriptorId);
                e.HasMany(x => x.DescriptorAbilities);

                e.Property(x => x.Name)
                    .HasMaxLength(30);

                e.Property(x => x.Description)
                    .HasMaxLength(1000);
            });

            builder.Entity<DescriptorAbility>(e =>
            {
                e.HasKey(x => x.DescriptorAbilityId);
                e.HasOne(x => x.Descriptor)
                    .WithMany(x => x.DescriptorAbilities)
                    .HasForeignKey(x => x.DescriptorId);

                e.Property(x => x.Name)
                    .HasMaxLength(30);

                e.Property(x => x.Description)
                    .HasMaxLength(1000);

                e.Property(x => x.Source)
                    .HasMaxLength(20);
            });

            builder.Entity<Focus>(e =>
            {
                e.HasKey(x => x.FocusId);
                e.HasMany(x => x.FocusAbilities);

                e.Property(x => x.Name)
                    .HasMaxLength(30);

                e.Property(x => x.Description)
                    .HasMaxLength(1000);
            });

            builder.Entity<FocusAbility>(e =>
            {
                e.HasKey(x => x.FocusAbilityId);
                e.HasOne(x => x.Focus)
                    .WithMany(x => x.FocusAbilities)
                    .HasForeignKey(x => x.FocusId);

                e.Property(x => x.Name)
                    .HasMaxLength(30);

                e.Property(x => x.Description)
                    .HasMaxLength(1000);

                e.Property(x => x.Source)
                    .HasMaxLength(20);
            });
        }
    }
}
