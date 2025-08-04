using NotificationService.Infrastructure.Caching.Interfaces;
using StackExchange.Redis;

namespace NotificationService.Infrastructure.Caching
{
    public class NotificationCacheManager : INotificationCacheInvalidator
    {
        private readonly IConnectionMultiplexer _redis;
        private readonly string _prefix;

        public NotificationCacheManager(
            IConnectionMultiplexer redis, 
            string prefix)
        {
            _redis = redis;
            _prefix = prefix;
        }

        public async Task InvalidateCacheForNotificationsAsync()
        {
            var db = _redis.GetDatabase();
            var endpoints = _redis.GetEndPoints();

            foreach (var endpoint in endpoints)
            {
                var server = _redis.GetServer(endpoint);
                var keys = server.Keys(pattern: $"{_prefix}*");
                await db.KeyDeleteAsync(keys.ToArray());
            }
        }
    }
}
