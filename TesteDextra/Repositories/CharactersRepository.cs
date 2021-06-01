using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteDextra.Models;
using TesteDextra.Repositories.Interfaces;
using MongoDB.Driver;
using MongoDB.Bson;

namespace TesteDextra.Repositories
{
    public class CharactersRepository : ICharactersRepository
    {
        private readonly IMongoCollection<Character> _collection;

        public CharactersRepository(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("PotterDb"));
            var db = client.GetDatabase("PotterDb");
            _collection = db.GetCollection<Character>(nameof(Character));

        }
        public bool Create(Character character)
        {
            _collection.InsertOne(character);
            return true;
        }

        public Character Get(Guid id) => _collection.Find(p => p.Id == id).FirstOrDefault();

        public bool Update(Guid id, Character character) => _collection.ReplaceOne(p => p.Id == id, character).IsAcknowledged;

        public bool Delete(Guid id) => _collection.DeleteOne(p => p.Id == id).IsAcknowledged;

        public IEnumerable<Character> List(Guid? house = null)
        {
            if (house.HasValue)
            {
                return _collection.Find(p => p.House == house).ToEnumerable();
            }
            return _collection.AsQueryable();
        }
    }
}
