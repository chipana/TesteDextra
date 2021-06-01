//using Microsoft.Extensions.Configuration;
//using MongoDB.Driver;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using TesteDextra.Storage.Interfaces;

//namespace TesteDextra.Storage
//{
//    public class MongoProvider : IStorageProvider 
//    {
//        public const string DATABASE_NAME = "PotterDb";

//        public readonly MongoClient Client;
//        public readonly IMongoDatabase Database;

//        public MongoProvider(IConfiguration configuration)
//        {
//            Client = new MongoClient(configuration.GetConnectionString(DATABASE_NAME));
//            Database = Client.GetDatabase(DATABASE_NAME);   
//        }

//        //public IEnumerable<object> GetAll<TClass>()
//        //{
//        //    TODO
//        //}
//    }
//}
