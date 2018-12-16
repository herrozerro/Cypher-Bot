using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CypherBot.DataAccess.Repos;

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
            using (var db = new CypherContext())
            {
                try
                {
                    Console.WriteLine("Clearing Database...");
                    Console.WriteLine("Clearing Cyphers.");
                    db.Cyphers.RemoveRange(db.Cyphers.ToList());
                    await db.SaveChangesAsync();
                    Console.WriteLine("Cyphers Cleared!");

                    Console.WriteLine("Clearing Artifacts.");
                    db.Artifacts.RemoveRange(db.Artifacts.ToList());
                    await db.SaveChangesAsync();
                    Console.WriteLine("Artifacts Cleared!");

                    Console.WriteLine("Getting Cyphers from cyphers.json");
                    var cypherStrings = await Data.FileIO.GetFileString("cyphers");

                    Console.WriteLine("Parsing Cyphers.");
                    var cyphers = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Models.Cypher>>(cypherStrings);

                    Console.WriteLine($"{cyphers.Count()} cyphers found! Adding.");
                    db.AddRange(cyphers);
                    await db.SaveChangesAsync();

                    Console.WriteLine("Getting Artifacts from artifacts.json");
                    var artifactStrings = await Data.FileIO.GetFileString("artifacts");

                    Console.WriteLine("Parsing Artifacts.");
                    var artifacts = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Models.Artifact>>(artifactStrings);

                    Console.WriteLine($"{artifacts.Count()} artifacts found! Adding.");
                    db.AddRange(artifacts);
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
