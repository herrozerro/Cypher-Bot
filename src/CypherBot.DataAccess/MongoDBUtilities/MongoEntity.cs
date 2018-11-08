using System;
using System.Collections.Generic;
using System.Text;
using MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CypherBot.DataAccess.MongoDBUtilities
{
    public class MongoEntity<T>
    {
        public ObjectId _id { get; set; }
        public T Object { get; set; }
    }
}
