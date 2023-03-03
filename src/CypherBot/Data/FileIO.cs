using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CypherBot.Data
{
    public static class FileIO
    {
        public static async Task<string> GetFileString(string fileName, string database = "")
        {
            string dataDir = AppDomain.CurrentDomain.GetData("DataDirectory").ToString();
            
            var ext = ".json";

            var path = new List<string>();
            path.Add(dataDir);
            if (database != null && database.Length != 0)
            {
                path.Add(database);
            }
            path.Add($"{fileName}{ext}");

            if (!File.Exists(string.Join(Path.DirectorySeparatorChar, path)))
            {
                return null;
            }

            var str = await File.ReadAllTextAsync(string.Join(Path.DirectorySeparatorChar, path));

            return str;
        }
    }
}
