using System.Threading.Tasks;
using StackExchange.Redis;

namespace RedisTr.redis
{
    public class RedisConnectioSinglton: IRedisConnectionE
    {
        private readonly ConnectionMultiplexer _connectionMultiplexer;
        private readonly IServer _server=null;
        public RedisConnectioSinglton()
        {
            Task<ConnectionMultiplexer> redis = ConnectionMultiplexer.ConnectAsync(
                new ConfigurationOptions//host.docker.internal
                {
#if DEBUG

#else
Password = "",
#endif
                    EndPoints = { "host.docker.internal:6379" },
                    
                    AbortOnConnectFail = false,
                });
            _connectionMultiplexer = redis.Result;
            _server= _connectionMultiplexer.GetServer("host.docker.internal:6379");
        }

        public ConnectionMultiplexer GConnectionMultiplexer()
        {
            return _connectionMultiplexer;
        }

        public IServer GetServer()
        {
            return _server;
        }
    }

    public interface IRedisConnectionE
    {
        ConnectionMultiplexer GConnectionMultiplexer();
        IServer GetServer();
    }
}
