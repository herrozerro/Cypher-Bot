using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CypherBot.Models;
using System.Threading.Tasks;

namespace CypherBot.Data
{
    public static class CypherList
    {
        private static List<Cypher> _Cyphers;
        public static async Task<List<Cypher>> Cyphers()
        {
            if (_Cyphers == null)
            {
                var s = await Data.FileIO.GetFileString("cyphers");

                var cyphers = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<Cypher>>(s);

                _Cyphers = cyphers.ToList();
            }

            return _Cyphers;
        }

        public static async Task<Cypher> GetRandomCypherAsync()
        {
            var rnd = new Random(Guid.NewGuid().GetHashCode());
            var cyphers = await Data.CypherList.Cyphers();
            return cyphers[rnd.Next(1, cyphers.Count)];
        }

        public static async Task<IEnumerable<Cypher>> GetRandomCyphersAsync(int numberOfCyphers)
        {
            var ls = new List<Cypher>();
            var rnd = new Random(Guid.NewGuid().GetHashCode());

            for (int i = 0; i < numberOfCyphers; i++)
            {
                var cypherList = await Cyphers();
                ls.Add(cypherList[rnd.Next(1, cypherList.Count)]);
            }

            return ls;
        }
    }
}
