using System;
using System.Collections.Generic;
using System.Text;

namespace CypherBot.DataAccess.Abstractions
{
    public abstract class IOService
    {
        private readonly string _connectionString;

        public string ConnectionString => _connectionString;

        public abstract IEnumerable<T> GetDocuments<T>(string Collection);
        public abstract IEnumerable<T> FilterDocuments<T>(string Collection, Dictionary<string, string> Filters);
        public abstract void StoreDocuments<T>(string Collection, IEnumerable<T> ObjToStore);
        public abstract void StoreDocument<T>(string Collection, T ObjToStore);

        public IOService(string ConnectionString)
        {
            _connectionString = ConnectionString;
        }

        public enum ServiceTypes
        {
            File = 1,
            Mongo = 2,
            RavenDB = 3
        }

        public static IOService BuildService(string connectionString, ServiceTypes ServiceType)
        {
            switch (ServiceType)
            {
                case IOService.ServiceTypes.File:
                    return new IO.FileIOService(connectionString);
                case IOService.ServiceTypes.Mongo:
                    return new IO.MongoIOService(connectionString);
                case IOService.ServiceTypes.RavenDB:
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
