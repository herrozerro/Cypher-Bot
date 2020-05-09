using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CypherBot.Core.Models;
using CypherBot.Core.DataAccess.Repos;

namespace CypherBot.Core.Services
{
    public static class CypherHelper
    {
        public static async Task<List<Cypher>> GetAllCyphersAsync()
        {
            using (var db = new CypherContext())
            {
                var cyList = await db.Cyphers.Include(x => x.EffectOptions).Include(x => x.Forms).ToListAsync();

                return cyList;
            }
        }

        public static async Task<Cypher> GetRandomCypherAsync()
        {
            var cyList = await GetAllCyphersAsync();

            var i = new Random().Next(0, cyList.Count() - 1);

            return cyList[i];
        }

        public static async Task<List<Cypher>> GetRandomCypherAsync(int numberOfCyphers)
        {
            var ls = new List<Cypher>();
            var rnd = RandomGenerator.GetRandom();

            for (int i = 0; i < numberOfCyphers; i++)
            {
                var cypherList = await GetAllCyphersAsync();
                ls.Add(cypherList[rnd.Next(1, cypherList.Count)]);
            }

            return ls;
        }

        public static async Task SaveUnidentifiedCypher(UnidentifiedCypher unidentifiedCypher)
        {
            using (var db = new CypherContext())
            {
                db.UnidentifiedCyphers.Add(unidentifiedCypher);
                await db.SaveChangesAsync();
            }
        }

        public static async Task SaveUnidentifiedArtifact(UnidentifiedArtifact unidentifiedArtifact)
        {
            using (var db = new CypherContext())
            {
                db.UnidentifiedArtifacts.Add(unidentifiedArtifact);
                await db.SaveChangesAsync();
            }
        }

        public static async Task RemoveUnidentifiedCypher(int unidentifiedCypherID)
        {
            using (var db = new CypherContext())
            {
                var uCypherToRemove = new UnidentifiedCypher() { UnidentifiedCypherId = unidentifiedCypherID };
                db.UnidentifiedCyphers.Remove(uCypherToRemove);
                await db.SaveChangesAsync();
            }
        }

        public static async Task RemoveUnidentifiedArtifact(int unidentifiedArtifactID)
        {
            using (var db = new CypherContext())
            {
                var uArtifactToRemove = new UnidentifiedArtifact() { UnidentifiedArtifactId = unidentifiedArtifactID };
                db.UnidentifiedArtifacts.Remove(uArtifactToRemove);
                await db.SaveChangesAsync();
            }
        }

        public static async Task SaveUnidentifiedCypherAsync(UnidentifiedCypher unidentifiedCypher)
        {
            using (var db = new CypherContext())
            {
                db.UnidentifiedCyphers.Add(unidentifiedCypher);

                await db.SaveChangesAsync();
            }
        }

        public static async Task<List<UnidentifiedCypher>> GetAllUnidentifiedCyphersAsync()
        {
            using (var db = new CypherContext())
            {
                var cyList = await db.UnidentifiedCyphers.ToListAsync();

                return cyList;
            }
        }
    }
}
