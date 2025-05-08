using StackExchange.Redis;
using StudentHousingHub.Core.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StudentHousingHub.Service.Services.Cashes
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase _database;
        public CacheService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }


        public async Task<string> GetCacheKeyAsync(string Key)
        {
            var casheResponse = await _database.StringGetAsync(Key);
            if (casheResponse.IsNullOrEmpty) return null;
            return casheResponse.ToString();
        }

        public async Task SetCacheKeyAsync(string Key, object response, TimeSpan expireTime)
        {
            if (response is null) return;

            var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            await _database.StringSetAsync(Key, JsonSerializer.Serialize(response, options), expireTime);
        }
    }
}
