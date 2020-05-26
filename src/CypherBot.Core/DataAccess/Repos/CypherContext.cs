using Microsoft.EntityFrameworkCore;
using CypherBot.Core.Models;
using System;

namespace CypherBot.Core.DataAccess.Repos
{
    public class CypherContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlite("Data Source=cs.db");
            optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("postgresConnectionString"));
        }

        public DbSet<Character> Characters { get; set; }
        public DbSet<CharacterInventory> CharacterInventories { get; set; }
        public DbSet<CharacterRecoveryRoll> CharacterRecoveryRolls { get; set; }
        public DbSet<CharacterCypher> CharacterCyphers { get; set; }
        public DbSet<CharacterAbility> CharacterAbilities { get; set; }
        public DbSet<CharacterArtifact> CharacterArtifacts { get; set; }
        public DbSet<CharacterPool> CharacterPools { get; set; }

        public DbSet<Cypher> Cyphers { get; set; }
        public DbSet<UnidentifiedCypher> UnidentifiedCyphers { get; set; }
        public DbSet<CypherFormOption> CypherFormOptions { get; set; }
        public DbSet<CypherEffectOption> CypherEffectOptions { get; set; }
        public DbSet<Artifact> Artifacts { get; set; }
        public DbSet<UnidentifiedArtifact> UnidentifiedArtifacts { get; set; }
        public DbSet<ArtifactQuirk> ArtifactQuirks { get; set; }
        public DbSet<Oddity> Oddities { get; set; }

        public DbSet<Creature> Creatures { get; set; }

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
            });

            builder.Entity<CharacterCypher>(e =>
            {
                e.HasKey(x => x.CypherId);

                e.HasOne(x => x.Character)
                    .WithMany(x => x.Cyphers)
                    .HasForeignKey(x => x.CharacterId);

            });

            builder.Entity<CharacterRecoveryRoll>(e =>
            {
                e.HasKey(x => x.RecoveryRollId);

                e.HasOne(x => x.Character)
                    .WithMany(x => x.RecoveryRolls)
                    .HasForeignKey(x => x.CharacterId);
            });

            builder.Entity<CharacterInventory>(e =>
            {
                e.HasKey(x => x.InventoryId);
                    
                e.HasOne(x => x.Character)
                    .WithMany(x => x.Inventory)
                    .HasForeignKey(x => x.CharacterId);
            });

            builder.Entity<CharacterAbility>(e =>
            {
                e.HasKey(x => x.CharacterAbilityId);

                e.HasOne(x => x.Character)
                    .WithMany(x => x.Abilities)
                    .HasForeignKey(x => x.CharacterId);
            });

            builder.Entity<CharacterArtifact>(e =>
            {
                e.HasKey(x => x.ArtifactId);

                e.HasOne(x => x.Character)
                    .WithMany(x => x.Artifacts)
                    .HasForeignKey(x => x.CharacterId);

            });

            builder.Entity<CharacterPool>(e =>
            {
                e.HasKey(x => x.PoolId);

                e.HasOne(x => x.Character)
                    .WithMany(x => x.Pools)
                    .HasForeignKey(x => x.CharacterId);

            });

            builder.Entity<Cypher>(e =>
            {
                e.HasKey(x => x.CypherId);

                e.HasMany(x => x.EffectOptions);

                e.HasMany(x => x.Forms);

                e.Ignore(x => x.Level);
            });

            builder.Entity<UnidentifiedCypher>(e =>
            {
                e.HasKey(x => x.UnidentifiedCypherId);
            });

            builder.Entity<CypherFormOption>(e =>
            {
                e.HasKey(x => x.FormOptionId);

                e.HasOne(x => x.Cypher)
                    .WithMany(x => x.Forms)
                    .HasForeignKey(x => x.CypherId);

            });

            builder.Entity<CypherEffectOption>(e =>
            {
                e.HasKey(x => x.EffectOptionId);

                e.HasOne(x => x.Cypher)
                    .WithMany(x => x.EffectOptions)
                    .HasForeignKey(x => x.CypherId);

            });

            builder.Entity<Artifact>(e =>
            {
                e.HasKey(x => x.ArtifactId);

                e.Ignore(x => x.Level);

            });

            builder.Entity<UnidentifiedArtifact>(e =>
            {
                e.HasKey(x => x.UnidentifiedArtifactId);
            });

            builder.Entity<ArtifactQuirk>(e =>
            {
                e.HasKey(x => x.ArtifactQuirkId);
            });

            builder.Entity<Oddity>(e =>
            {
                e.HasKey(x => x.OddityId);
            });

            builder.Entity<Creature>(e =>
            {
                e.HasKey(p => p.CreatureId);
            });
        }
    }
}
