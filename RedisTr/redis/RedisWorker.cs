using System;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RedisTr.models;
using StackExchange.Redis;

namespace RedisTr.redis
{
    public class RedisWorker
    {
        private readonly IRedisConnectionE _redisConnectionE;
        private readonly MDataIn _mDataIn;
        private readonly MDataOut _mDataOut;
        private readonly string _rabBody;
        public static string MyHost = "host.docker.internal";

        public RedisWorker(IRedisConnectionE redisConnectionE, MDataIn mDataIn, MDataOut mDataOut, string rabBody)
        {
            _redisConnectionE = redisConnectionE;
            _mDataIn = mDataIn;
            _mDataOut = mDataOut;
            _rabBody = rabBody;
        }
        public async Task Run()
        {
            long runner = 0;
            ISubscriber sub = _redisConnectionE.GConnectionMultiplexer().GetSubscriber();

            WrapperMessage m = new WrapperMessage {Data = _rabBody, Key = utils.Utils.RandomString(12)};

            await sub.SubscribeAsync($"canel:{100}:{1}:{m.Key}", (channel, message) =>
             {
                  sub.UnsubscribeAsync(channel.ToString());
                 _mDataOut.SetErrorData(ErrorCode.PrintError, message);
                 _mDataOut.InnerRunner.IsError = false;
                 Interlocked.Increment(ref runner);

             });
             await sub.PublishAsync($"canel:{_mDataIn.HotelId}:{_mDataIn.PosId}",JsonConvert.SerializeObject(m));

             var i = 0;

                 while (Interlocked.Read(ref runner)==0)
                 {
                     i++;
                     await Task.Delay(100);
                     if (i > 6)
                     {
                         var err = "Истек срок ожидания ответа от клиента";
                         _mDataOut.InnerRunner.IsError = true;
                         _mDataOut.InnerRunner.ErrorText =err;
                         break;
                     }
                 }
        }
    }

    public class WrapperMessage
    {
        public string Key { get; set; }
        public string Data { get; set; }

    }
}
