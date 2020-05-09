using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CypherBot.Core.Models;
using CypherBot.Core.DataAccess.Repos;

namespace CypherBot.Core.Services
{
    public static class ArtifactHelper
    {
        public static async Task<List<Artifact>> GetAllArtifactsAsync()
        {
            using (var db = new CypherContext())
            {
                var artList = await db.Artifacts.ToListAsync();

                return artList;
            }
        }

        public static async Task<Artifact> GetRandomArtifactAsync(string genre = "")
        {
            var artList = await GetAllArtifactsAsync();

            if (genre != "")
            {
                artList = artList.Where(x => x.Genre == genre).ToList();
            }

            var i = RandomGenerator.GetRandom().Next(0, artList.Count() - 1);

            return artList[i];
        }

        public static async Task<List<Artifact>> GetRandomArtifactAsync(int numberOfCyphers, string genre = "")
        {
            var ls = new List<Artifact>();
            var rnd = RandomGenerator.GetRandom();

            for (int i = 0; i < numberOfCyphers; i++)
            {
                var artifactList = await GetAllArtifactsAsync();

                if (genre != "")
                {
                    artifactList = artifactList.Where(x => x.Genre == genre).ToList();
                }

                ls.Add(artifactList[rnd.Next(1, artifactList.Count)]);
            }

            return ls;
        }

        public static async Task<List<ArtifactQuirk>> GetAllArtifactQuirksAsync()
        {
            using (var db = new CypherContext())
            {
                var artList = await db.ArtifactQuirks.ToListAsync();

                return artList;
            }
        }

        public static async Task<ArtifactQuirk> GetRandomArtifactQuirkAsync()
        {
            var artQuiList = await GetAllArtifactQuirksAsync();

            var i = RandomGenerator.GetRandom().Next(0, artQuiList.Count() - 1);

            return artQuiList[i];
        }

        public static async Task<List<UnidentifiedArtifact>> GetAllUnidentifiedArtifactsAsync()
        {
            using(var db = new CypherContext())
            {
                var artifacts = await db.UnidentifiedArtifacts.ToListAsync();
                return artifacts;
            }
        }

        public static async Task SaveUnidentifiedArtifactAsync(UnidentifiedArtifact unidentifiedArtifact)
        {
            using (var db = new CypherContext())
            {
                db.UnidentifiedArtifacts.Add(unidentifiedArtifact);
                await db.SaveChangesAsync();
            }
        }

        public static async Task RemoveUnidentifiedArtifactAsync(int unidentifiedArtifactID)
        {
            using (var db = new CypherContext())
            {
                var uArtifactToRemove = new UnidentifiedArtifact() { UnidentifiedArtifactId = unidentifiedArtifactID };
                db.UnidentifiedArtifacts.Remove(uArtifactToRemove);
                await db.SaveChangesAsync();
            }
        }

    }
}
