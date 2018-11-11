using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using CypherBot.DataAccess.Abstractions;

namespace CypherBot.DataAccess.IO
{
    public class FileIOService : IOService
    {
        public async Task<string> GetFileString(string database, string fileName)
        {
            string dataDir = ConnectionString;
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
        }

        public async Task SaveTextFileAsync(string database, string fileName, string obj)
        {
            fileName = Utilities.StringHelpers.CleanFileName(fileName);
            database = Utilities.StringHelpers.CleanPathName(database);

            fileName = fileName.Replace(" ", string.Empty);

            string dataDir = ConnectionString;
            var ext = ".json";

            var path = new List<string>();
            path.Add(dataDir);
            if (database != null && database.Length != 0)
            {
                path.Add(database);
            }
            path.Add(fileName+ext);

            var fullPath = string.Join('\\', path);

            var dir = Path.GetDirectoryName(fullPath);

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            //var s = JsonConvert.SerializeObject(obj);

            await File.WriteAllTextAsync(fullPath, obj);
        }

        public static List<string> GetFilesInDatabase(string database)
        {
            string dataDir = AppDomain.CurrentDomain.GetData("DataDirectory").ToString();

            database = Utilities.StringHelpers.CleanPathName(database);

            var path = dataDir + "\\" + database;

            if (!Directory.Exists(path))
            {
                return null;
            }

            return Directory.GetFiles(path).Select(x=> Path.GetFileNameWithoutExtension(x)).ToList();
        }

        public override IEnumerable<T> GetDocuments<T>(string Collection)
        {
            string file = "none";
            if(typeof(T) == typeof(Models.Character))
            {
                file = "Characters";
            }
            if (typeof(T) == typeof(Models.Cypher))
            {
                file = "cyphers";
                Collection = "";
            }

            string dataDir = ConnectionString;

            Task<string> t = Task.Run(() => GetFileString(Collection, file));

            Task.WaitAll(t);

            var str = t.Result;

            var objs = JsonConvert.DeserializeObject<List<T>>(str);

            return objs;
        }

        public override void StoreDocuments<T>(string Collection, IEnumerable<T> ObjToStore)
        {
            string file = "none";
            if (typeof(T) == typeof(Models.Character))
            {
                file = "Characters";
            }
            if (typeof(T) == typeof(Models.Cypher))
            {
                file = "cyphers";
                Collection = "";
            }

            string obj = JsonConvert.SerializeObject(ObjToStore);

            string dataDir = ConnectionString;

            Task t = Task.Run(() => SaveTextFileAsync(Collection, file, obj));

            Task.WaitAll(t);
        }

        public override void StoreDocument<T>(string Collection, T ObjToStore)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<T> FilterDocuments<T>(string Collection, Dictionary<string, string> Filters)
        {
            throw new NotImplementedException();
        }

        public FileIOService(string filepath) : base(filepath)
        {

        }
    }
}
