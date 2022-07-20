using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using RedisTr.models;
using RedisTr.redis;

namespace RedisTr.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiCore : ControllerBase
    {
        private readonly IRedisConnectionE _connection;

        public ApiCore(IRedisConnectionE connection)
        {
            _connection = connection;
        }
        [HttpPost]
        public async Task<MDataOut> PostCore([FromBody] MDataIn mDataIn)
        {
            MDataOut mDataOut=new MDataOut(mDataIn);
            var r = new StreamReader(Request.Body);
            r.BaseStream.Seek(0, SeekOrigin.Begin);
            var  rawBody=await r.ReadToEndAsync();
           
            

            if (string.IsNullOrEmpty(rawBody)) throw new Exception("Не могу олучить тело запроса");
            RedisWorker redisWorker=new RedisWorker(_connection,mDataIn,mDataOut, rawBody);
            var task =   Task.Run(() => redisWorker.Run());
            await Task.WhenAll(task);
            

            if (mDataOut.InnerRunner.IsError == false)
            {
                Console.WriteLine($" ############ {mDataOut.ErrorText}");
                return mDataOut;//Успешное выполнение печати или ошибка на клиенте (смотреть содержание ответа)
            }

            mDataOut.SetErrorData(ErrorCode.PrintError,mDataOut.InnerRunner.ErrorText);
            return mDataOut;

            

          
            //r.QueuePurge();
            // Ошибка выполнеения, сервер
         


        }
    }

}
