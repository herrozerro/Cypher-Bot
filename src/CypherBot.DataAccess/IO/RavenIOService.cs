using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Raven;
using Raven.Client;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using CypherBot.DataAccess.Abstractions;

namespace CypherBot.DataAccess.IO
{
    public class RavenIOService : IOService
    {
        public RavenIOService(string ConnectionString) : base(ConnectionString)
        {
        }

        public static  string GetDocument(string database, string document)
        {
            using (IDocumentStore store = new DocumentStore
            {
                Urls = new[]                        // URL to the Server,
                        {                                   // or list of URLs 
                            "http://localhost:38888"  // to all Cluster Servers (Nodes)
                        },
                Database = "cypherData"           // Default database that DocumentStore will interact with
                
            }.Initialize())
            {
                using (IDocumentSession session = store.OpenSession())
                {
                    try
                    {
                        var ent = new CypherBot.Models.Character() { Name = "Bobble", Player = "sam" };
                        session.Store(ent);
                        session.SaveChanges();
                        Console.WriteLine(ent);
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex.Message);

                    }

                    return null;
                }
            }
        }

        public override IEnumerable<T> FilterDocuments<T>(string Collection, Dictionary<string, string> Filters)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<T> GetDocuments<T>(string Collection)
        {
            throw new NotImplementedException();
        }

        public override void StoreDocument<T>(string Collection, T ObjToStore)
        {
            throw new NotImplementedException();
        }

        public override void StoreDocuments<T>(string Collection, IEnumerable<T> ObjToStore)
        {
            throw new NotImplementedException();
        }
    }
}
