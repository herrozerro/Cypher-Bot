using CypherBot.Core.DataAccess.Repos;
using CypherBot.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CypherBot.Utilities
{
    public static class OddityHelper
    {
        public static async Task<List<Oddity>> GetAllOdditysAsync()
        {
            using (var db = new CypherContext())
            {
                var oddList = await db.Oddities.ToListAsync();

                return oddList;
            }
        }

        public static async Task<Oddity> GetRandomOddityAsync()
        {
            var oddList = await GetAllOdditysAsync();

            var i = new Random().Next(0, oddList.Count() - 1);

            return oddList[i];
        }

        public static async Task<List<Oddity>> GetRandomOddityAsync(int numberOfCyphers)
        {
            var ls = new List<Oddity>();
            var rnd = new Random(Guid.NewGuid().GetHashCode());

            for (int i = 0; i < numberOfCyphers; i++)
            {
                var OddityList = await GetAllOdditysAsync();

                ls.Add(OddityList[rnd.Next(1, OddityList.Count)]);
            }

            return ls;
        }
    }
}
