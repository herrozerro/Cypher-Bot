using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CypherBot.Core.DataAccess.Repos;
using CypherBot.Core.Models;

namespace CypherBot.Utilities
{
    public static class DatabaseHelper
    {
        /// <summary>
        /// Loads reference data from datafiles
        /// </summary>
        public static async Task InitializeDatabaseAsync()
        {
            using (var db = new CypherContext())
            {
                try
                {
                    #region DatabaseClear
                    Console.WriteLine("Clearing Database...");

                    Console.WriteLine("Clearing Cyphers.");
                    db.Cyphers.RemoveRange(db.Cyphers.ToList());
                    await db.SaveChangesAsync();
                    Console.WriteLine("Cyphers Cleared!");

                    Console.WriteLine("Clearing Artifacts.");
                    db.Artifacts.RemoveRange(db.Artifacts.ToList());
                    await db.SaveChangesAsync();
                    Console.WriteLine("Artifacts Cleared!");

                    Console.WriteLine("Clearing Oddities.");
                    db.Oddities.RemoveRange(db.Oddities.ToList());
                    await db.SaveChangesAsync();
                    Console.WriteLine("Oddities Cleared!");

                    Console.WriteLine("Clearing Artifact Quirks.");
                    db.ArtifactQuirks.RemoveRange(db.ArtifactQuirks.ToList());
                    await db.SaveChangesAsync();
                    Console.WriteLine("Artifact Quirks Cleared!"); 
                    #endregion

                    //Cyphers
                    #region CypherLoad
                    Console.WriteLine("Getting Cyphers from cyphers.json");
                    var cypherStrings = await Data.FileIO.GetFileString("cyphers");

                    Console.WriteLine("Parsing Cyphers.");
                    var cyphers = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cypher>>(cypherStrings);

                    Console.WriteLine($"{cyphers.Count()} cyphers found! Adding.");
                    db.AddRange(cyphers);
                    await db.SaveChangesAsync();

                    var ls = db.CypherFormOptions.ToList();
                    #endregion

                    //Artifacts
                    #region ArtifactLoad
                    Console.WriteLine("Getting Artifacts from artifacts.json");
                    var artifactStrings = await Data.FileIO.GetFileString("artifacts");

                    Console.WriteLine("Parsing Artifacts.");
                    var artifacts = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Artifact>>(artifactStrings);

                    Console.WriteLine($"{artifacts.Count()} artifacts found! Adding.");
                    db.AddRange(artifacts);
                    await db.SaveChangesAsync();
                    #endregion

                    //Oddities
                    #region OddityLoad
                    Console.WriteLine("Getting Oddities from oddities.json");
                    var odditieStrings = await Data.FileIO.GetFileString("oddities");

                    Console.WriteLine("Parsing Artifacts.");
                    var oddities = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Oddity>>(odditieStrings);

                    Console.WriteLine($"{oddities.Count()} oddities found! Adding.");
                    db.AddRange(oddities);
                    await db.SaveChangesAsync();
                    #endregion

                    //Artifact Quirks
                    #region ArtifactQuirkLoad
                    Console.WriteLine("Getting Artifact Quirks from artifactquirks.json");
                    var artifactQuirkStrings = await Data.FileIO.GetFileString("artifactquirks");

                    Console.WriteLine("Parsing Artifacts.");
                    var artifactQuirks = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ArtifactQuirk>>(artifactQuirkStrings);

                    Console.WriteLine($"{artifactQuirks.Count()} artifact quirks found! Adding.");
                    db.AddRange(artifactQuirks);
                    await db.SaveChangesAsync(); 
                    #endregion
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
