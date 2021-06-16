using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TesteDextra.Models;
using TesteDextra.Providers.Redis;
using TesteDextra.Repositories.Interfaces;

namespace TesteDextra.Repositories
{
    /// <summary>
    /// Potter's Houses repository
    /// </summary>
    public class HousesRepository : RedisProvider, IHousesRepository
    {
        private const string POTTER_CACHE_KEY = "PotterHouses";
        private readonly string _apiKey;
        private readonly string _apiUrl;

        private readonly HttpClient _httpClient;

        /// <summary>
        /// Potter's Houses repository constructor
        /// </summary>
        /// <param name="configuration"></param>
        public HousesRepository(IConfiguration configuration) 
            : base (configuration.GetConnectionString("PotterCache"))
        {
            var section = configuration.GetSection("ApiDefinitions");
            _apiKey = section.GetValue<string>("ApiKey");
            _apiUrl = section.GetValue<string>("ApiUrl");

            _httpClient = new HttpClient();
        }

        /// <summary>
        /// Gets houses from repository
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<House>> GetHousesAsync()
        {
            var cache = await GetValueFromKeyAsync(POTTER_CACHE_KEY);
            if (string.IsNullOrWhiteSpace(cache))
            {
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("apiKey", _apiKey);

                var response = _httpClient.GetAsync(_apiUrl).Result;
                if (response.IsSuccessStatusCode)
                {
                    cache = response.Content.ReadAsStringAsync().Result;
                    await SetValueAsync(POTTER_CACHE_KEY, cache, TimeSpan.FromMinutes(15));
                }
            }

            JObject housesJson = JObject.Parse(cache);

            return JsonConvert.DeserializeObject<List<House>>(housesJson["houses"].ToString());
        }
    }
}
