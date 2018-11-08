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
            var s = await DataAccess.IO.FileIOService.GetFileString(database, fileName);

            return s;
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
