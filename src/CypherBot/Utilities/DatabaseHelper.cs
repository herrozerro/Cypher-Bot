using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CypherBot.Utilities
{
    public static class DatabaseHelper
    {
        /// <summary>
        /// Loads reference data from datafiles
        /// </summary>
        /// <returns></returns>
        public static async Task InitializeDatabaseAsync()
        {
            using (var db = new DataAccess.Repos.CypherContext())
            {
                try
                {
                    Console.WriteLine("Clearing Database...");
                    Console.WriteLine("Clearing Cyphers.");
                    db.Cyphers.RemoveRange(db.Cyphers.ToList());
                    await db.SaveChangesAsync();

                    Console.WriteLine("Cyphers Cleared!");

                    Console.WriteLine("Getting Cyphers from cyphers.json");
                    var cypherStrings = await Data.FileIO.GetFileString("cyphers");

                    Console.WriteLine("Parsing Cyphers.");
                    var cyphers = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Models.Cypher>>(cypherStrings);

                    Console.WriteLine($"{cyphers.Count()} cyphers found! Adding.");
                    db.AddRange(cyphers);
                    await db.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
        }



    }
}
