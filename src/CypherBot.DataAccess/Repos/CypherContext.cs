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
using CypherBot.Models;

namespace CypherBot.DataAccess.Repos
{
    public class CypherContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=cs.db");


        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Character>(e =>
            {
                e.HasKey(x => x.CharacterId);
                e.HasMany(x => x.Inventory);
                e.HasMany(x => x.RecoveryRolls);
                e.HasMany(x => x.Cyphers);
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

                e.Ignore(x => x.Level);

            });

            builder.Entity<CharacterRecoveryRoll>(e =>
            {
                e.HasKey(x => x.RecoveryRollId);
                e.Property(x => x.RollName)
                    .HasMaxLength(25);
            });

            builder.Entity<CharacterInventory>(e =>
            {
                e.HasKey(x => x.InventoryId);
                e.Property(x => x.ItemName)
                    .HasMaxLength(50);
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

        }



        public DbSet<Character> Characters { get; set; }
        public DbSet<CharacterInventory> CharacterInventories { get; set; }
        public DbSet<CharacterRecoveryRoll> CharacterRecoveryRolls { get; set; }
        public DbSet<CharacterCypher> CharacterCyphers { get; set; }
        public DbSet<Cypher> Cyphers { get; set; }

    }
}
