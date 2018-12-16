using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CypherBot.Utilities
{
    public static class ArtifactHelper
    {
        public static async Task<List<Models.Artifact>> GetAllArtifactsAsync()
        {
            using (var db = new DataAccess.Repos.CypherContext())
            {
                var artList = await db.Artifacts.ToListAsync();

                return artList;
            }
        }

        public static async Task<Models.Artifact> GetRandomArtifactAsync(string genre = "")
        {
            var cyList = await GetAllArtifactsAsync();

            if (genre != "")
            {
                cyList = cyList.Where(x => x.Genre == genre).ToList();
            }

            var i = new Random().Next(0, cyList.Count() - 1);

            return cyList[i];
        }

        public static async Task<List<Models.Artifact>> GetRandomArtifactAsync(int numberOfCyphers, string genre = "")
        {
            var ls = new List<Models.Artifact>();
            var rnd = new Random(Guid.NewGuid().GetHashCode());

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
    }
}
