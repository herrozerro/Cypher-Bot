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
                e.HasKey(x => x.ID);
                e.HasMany(x=>x.Inventory);
                e.HasMany(x=>x.RecoveryRolls);
                e.HasMany(x=>x.Cyphers);
                e.Property(x => x.ID).ValueGeneratedOnAdd();
            });
        }



        public DbSet<Character> Characters { get; set; }
        public DbSet<CharacterInventory> CharacterInventories { get; set; }
        public DbSet<CharacterRecoveryRoll> CharacterRecoveryRolls { get; set; }
        public DbSet<Cypher> Cyphers { get; set; }

    }
}
