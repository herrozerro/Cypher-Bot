using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using MongoDB;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Newtonsoft.Json;
using CypherBot.DataAccess.MongoDBUtilities;
using System.Reflection;
using System.Linq.Expressions;
using CypherBot.DataAccess.Abstractions;

namespace CypherBot.DataAccess.IO
{
    public class MongoIOService : IOService
    {
        private IMongoClient _client;

        public IMongoClient Client => _client;

        public MongoIOService(string connection) : base(connection)
        {
            _client = new MongoClient(ConnectionString);
        }

        public override IEnumerable<T> GetDocuments<T>(string Collection)
        {
            var db = Client.GetDatabase("cypher_objects");

            var coll = db.GetCollection<MongoEntity<T>>(Collection);

            return coll.AsQueryable().Select(x => x.Object).ToList();
        }

        public override IEnumerable<T> FilterDocuments<T>(string collection, Dictionary<string,string> Filters)
        {
            var db = Client.GetDatabase("cypher_objects");

            //string cmdDoc = @"{find:'Object',filter: { 'Name' : '"+ nameFilter +"' }}";

            IQueryable<MongoEntity<T>> coll = db.GetCollection<MongoEntity<T>>(collection).AsQueryable();

            Type mType = typeof(MongoEntity<T>);
            Type type = typeof(T);



            foreach (PropertyInfo p in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                if (Filters.ContainsKey(p.Name))
                {
                    ParameterExpression parameter = Expression.Parameter(typeof(MongoEntity<T>),"mEntity");
                    MemberExpression member = Expression.MakeMemberAccess(parameter, mType.GetProperty("Object",BindingFlags.Instance|BindingFlags.Public));
                    member = Expression.MakeMemberAccess(member, p);

                    ConstantExpression mConst = Expression.Constant(Convert.ChangeType(Filters[p.Name], p.PropertyType));
                    BinaryExpression bExp = Expression.Equal(member, mConst);

                    Expression<Func<MongoEntity<T>, bool>> lamdaExp = Expression.Lambda<Func<MongoEntity<T>, bool>>(bExp, parameter);

                    coll = coll.Where(lamdaExp);
                }
            }

            return coll.Select(x => x.Object).ToList();
        }

        public override void StoreDocument<T>(string Collection, T ObjToStore)
        {
            var db = Client.GetDatabase("cypher_objects");

            var coll = db.GetCollection<MongoEntity<T>>(Collection);

            var mongoent = new MongoEntity<T> { Object = ObjToStore };

            coll.InsertOne(mongoent);

        }

        public override void StoreDocuments<T>(string Collection, IEnumerable<T> ObjToStore)
        {
            throw new NotImplementedException();
        }
        
    }
}
