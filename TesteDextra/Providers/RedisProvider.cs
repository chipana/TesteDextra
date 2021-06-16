using System;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace TesteDextra.Providers.Redis
{
    /// <summary>
    /// Redis cache provider
    /// </summary>
    public abstract class RedisProvider
    {
        /// <summary>
        /// Redis cache connection multiplexer
        /// </summary>
        protected readonly ConnectionMultiplexer connectionMultiplexer;

        /// <summary>
        /// Redis cache database
        /// </summary>
        protected readonly IDatabase database;

        /// <summary>
        /// Redis cache provider constructor
        /// </summary>
        /// <param name="connectionString"></param>
        public RedisProvider(string connectionString)
        {
            connectionMultiplexer = ConnectionMultiplexer.Connect(connectionString);
            database = connectionMultiplexer.GetDatabase();
        }

        /// <summary>
        /// Gets value from key in redis cache database
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<string> GetValueFromKeyAsync(string key) =>  await database.StringGetAsync(key);

        /// <summary>
        /// Sets key and value in redis cache database
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="ttl"></param>
        /// <returns></returns>
        public async Task<bool> SetValueAsync(string key, string value, TimeSpan ttl) => await database.StringSetAsync(key, value, ttl);
        
    }
}
