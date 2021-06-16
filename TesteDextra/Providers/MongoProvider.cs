using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace TesteDextra.Storage
{
    /// <summary>
    /// Mongos NoSql provider
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class MongoProvider<T>
        where T : class
    {
        /// <summary>
        /// Mongo Client
        /// </summary>
        protected readonly MongoClient Client;

        /// <summary>
        /// Mongo Database
        /// </summary>
        protected readonly IMongoDatabase Database;

        /// <summary>
        /// Mongo Collection
        /// </summary>
        protected readonly IMongoCollection<T> Collection;

        /// <summary>
        /// Mongos NoSql provider constructor
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="databaseName"></param>
        /// <param name="collectionName"></param>
        public MongoProvider(string connectionString, string databaseName, string collectionName)
        {
            Client = new MongoClient(connectionString);
            Database = Client.GetDatabase(databaseName);
            Collection = Database.GetCollection<T>(collectionName);
        }

        /// <summary>
        /// Creates a new object
        /// </summary>
        /// <param name="insertObject"></param>
        /// <param name="insertOneOptions"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task CreateAsync(T insertObject, InsertOneOptions insertOneOptions = null, CancellationToken cancellationToken = default)
            => await Collection.InsertOneAsync(insertObject, insertOneOptions, cancellationToken);

        /// <summary>
        /// Gets an object
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<T> GetAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default)
            => await Collection.Find(filter).FirstOrDefaultAsync<T>(cancellationToken);

        /// <summary>
        /// Updates an object
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="updatedObject"></param>
        /// <param name="replaceOptions"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ReplaceOneResult> UpdateAsync(Expression<Func<T, bool>> filter, T updatedObject, ReplaceOptions replaceOptions = null, CancellationToken cancellationToken = default)
            => await Collection.ReplaceOneAsync(filter, updatedObject, replaceOptions, cancellationToken);

        /// <summary>
        /// Deletes an object
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<DeleteResult> DeleteAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default)
            => await Collection.DeleteOneAsync(filter, cancellationToken);

        /// <summary>
        /// Retuns a list of objects
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="findOptions"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> filter, FindOptions<T> findOptions = null, CancellationToken cancellationToken = default)
            => (await Collection.FindAsync(filter, findOptions, cancellationToken)).ToEnumerable();
    }
}
