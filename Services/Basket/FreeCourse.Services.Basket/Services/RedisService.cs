using StackExchange.Redis;

namespace FreeCourse.Services.Basket.Services
{
    public class RedisService
    {
        private readonly string _host;
        private readonly int _port;

        private readonly string url;

        private ConnectionMultiplexer _redisConnection;
        public RedisService(string host, int port)
        {
            _host = host;
            _port = port;

            url = $"{_host}:{_port}";
        }

        public void Connect() => _redisConnection = ConnectionMultiplexer.Connect(url);

        public IDatabase GetDatabase(int db = 1) => _redisConnection.GetDatabase(db);

    }
}
