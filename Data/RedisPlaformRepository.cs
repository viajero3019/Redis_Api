
using System.Text.Json;
using REDIS_Api.Models;
using StackExchange.Redis;

namespace REDIS_Api.Data
{
    public class RedisPlaformRepository : IPlatformRepository
    {
        private readonly IConnectionMultiplexer _redisConn;

        public RedisPlaformRepository(IConnectionMultiplexer redisConn)
        {
            _redisConn = redisConn;
        }

        public void CreatePlatform(Platform platform)
        {
            if(platform == null) throw new ArgumentException(nameof(platform));

            var db = _redisConn.GetDatabase();

            var serializedPlatform = JsonSerializer.Serialize(platform);

            // db.StringSet(platform.Id, serializedPlatform);

            // db.SetAdd("PlatformsSet", serializedPlatform);

            db.HashSet("hashplatform", new HashEntry[] { new HashEntry(platform.Id, serializedPlatform)});
        }

        public IEnumerable<Platform?>? GetAllPlatforms()
        {
            var db = _redisConn.GetDatabase();

            // var completeSet = db.SetMembers("PlatformsSet");

            var completeHash = db.HashGetAll("hashplatform");

            // if(completeSet.Length > 0)
            if(completeHash.Length > 0)
            {
                // return Array.ConvertAll(completeSet, val => JsonSerializer.Deserialize<Platform>(val)).ToList();
                return Array.ConvertAll(completeHash, val => JsonSerializer.Deserialize<Platform>(val.Value)).ToList();
            }
            return null;
        }

        public Platform? GetPlatformById(string id)
        {
            var db = _redisConn.GetDatabase();

            // var platform = db.StringGet(id);

            var platformHash = db.HashGet("hashplatform", id);

            // if(!string.IsNullOrEmpty(platform)) 
            if(!string.IsNullOrEmpty(platformHash)) 
            {
                // return JsonSerializer.Deserialize<Platform>(platform);
                return JsonSerializer.Deserialize<Platform>(platformHash);
            }
            return null;
        }
    }
}