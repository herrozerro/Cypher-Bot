using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace CypherBot.Data
{
    public static class FileIO
    {
        public static async Task<string> GetFileString(string fileName, string database = "")
        {
            //var s = await DataAccess.IO.FileIOService.GetFileString(database, fileName);

            string dataDir = AppDomain.CurrentDomain.GetData("DataDirectory").ToString();
            var ext = ".json";
            var path = new List<string>();
            path.Add(dataDir);
            if (database != null && database.Length != 0)
            {
                path.Add(database);
            }
            path.Add($"{fileName}{ext}");

            if (!File.Exists(string.Join('\\', path)))
            {
                return null;
            }

            var str = await File.ReadAllTextAsync(string.Join('\\', path));

            return str;

            //return s;
        }

        public static async Task SaveFileString(string fileName, string playerId, string fileContent)
        {
            await CypherBot.DataAccess.IO.FileIOService.SaveTextFileAsync($"Players\\{playerId}", fileName, fileContent);
        }

        public static List<string> GetFilesInDatabase(string database)
        {
            return DataAccess.IO.FileIOService.GetFilesInDatabase(database);
        }
    }

}
