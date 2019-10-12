using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CypherBot.Core.Models;
using CypherBot.Core.DataAccess.Repos;

namespace CypherBot.Core.Utilities
{
    public static class CypherHelper
    {
        public static async Task<List<Cypher>> GetAllCyphersAsync()
        {
            using (var db = new CypherContext())
            {
                var cyList = await db.Cyphers.ToListAsync();

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
            var rnd = new Random(Guid.NewGuid().GetHashCode());

            for (int i = 0; i < numberOfCyphers; i++)
            {
                var cypherList = await GetAllCyphersAsync();
                ls.Add(cypherList[rnd.Next(1, cypherList.Count)]);
            }

            return ls;
        }
    }
}
