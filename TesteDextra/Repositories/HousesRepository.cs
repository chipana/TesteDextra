using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using TesteDextra.Models;
using TesteDextra.Repositories.Interfaces;

namespace TesteDextra.Repositories
{
    public class HousesRepository : IHousesRepository
    {
        public const string POTTER_CACHE_KEY = "PotterHouses";
        private readonly string _apiKey;
        private readonly string _apiUrl;

        private readonly HttpClient _httpClient;
        //private readonly IDatabase _redisDatabase;

        public HousesRepository(IConfiguration configuration)
        {
            var section = configuration.GetSection("ApiDefinitions");
            _apiKey = section.GetValue<string>("ApiKey");
            _apiUrl = section.GetValue<string>("ApiUrl");

            _httpClient = new HttpClient();

            //_redisDatabase = ConnectionMultiplexer.Connect(configuration.GetConnectionString("PotterCache")).GetDatabase();
        }

        public IEnumerable<House> GetHouses()
        {
            //var cache = _redisDatabase.StringGet(POTTER_CACHE_KEY);
            //if (cache.HasValue)
            //{
            string cache;
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("apiKey", _apiKey);

                var response = _httpClient.GetAsync(_apiUrl).Result;
                if (response.IsSuccessStatusCode)
                {
                    cache = response.Content.ReadAsStringAsync().Result;
                    //_redisDatabase.StringSet(POTTER_CACHE_KEY, cache, expiry: TimeSpan.FromMinutes(15));
                    JObject housesJson = JObject.Parse(cache);

                    return JsonConvert.DeserializeObject<List<House>>(housesJson["houses"].ToString());
                }
            //}

            //if (cache.HasValue)
            //{
            //}
            return new List<House>();
        }
    }
}
