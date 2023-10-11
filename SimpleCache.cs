namespace StatelessApi
{
    public interface ILogger
    {
        void Log(string message);
    }

    public interface IDataFetcher<T>
    {
        T FetchData(string key);
    }

    public class SimpleCache<T>
    {
        private readonly Dictionary<string, CacheItem> _cacheStore = new Dictionary<string, CacheItem>();
        private readonly TimeSpan _defaultCacheDuration = TimeSpan.FromMinutes(5);
        private readonly ILogger? _logger;
        private readonly IDataFetcher<T> _dataFetcher;

        public SimpleCache(ILogger logger, IDataFetcher<T> dataFetcher)
        {
            _logger = logger;
            _dataFetcher = dataFetcher ?? throw new Exception(nameof(dataFetcher));
        }

        public T Get(string key)
        {
            if (_cacheStore.ContainsKey(key))
            {
                var cacheItem = _cacheStore[key];
                if (cacheItem.Expiration > DateTime.UtcNow)
                {
                    _logger?.Log($"Cache hit for key: {key}");
                    return cacheItem.Value;
                }
                else
                {
                    _logger?.Log($"Cache expired for key: {key}");
                    _cacheStore.Remove(key);
                }
            }

            _logger?.Log($"Cache miss for key: {key}. Fetching data...");
            var data = _dataFetcher.FetchData(key);
            Set(key, data);
            return data;
        }

        public void Set(string key, T value)
        {
            Set(key, value, _defaultCacheDuration);
        }

        public void Set(string key, T value, TimeSpan duration)
        {
            var expiration = DateTime.Now.Add(duration);
            if (_cacheStore.ContainsKey(key))
            {
                _cacheStore[key] = new CacheItem { Value = value, Expiration = expiration };
            }
            else
            {
                _cacheStore.Add(key, new CacheItem { Value = value, Expiration = expiration });
            }
            _logger.Log($"Data set in cache for key: {key}");
        }

        private class CacheItem
        {
            public T Value { get; set; }
            public DateTime Expiration { get; set; }
        }
    }
}
