
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace RedisTr
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseKestrel(options =>
                {
                    options.Limits.MaxConcurrentConnections = 200;
                    options.Limits.MaxRequestBodySize = 10 * 1024;
                    options.Limits.MinRequestBodyDataRate = null;
                    options.Limits.MinResponseDataRate =
                        new MinDataRate(bytesPerSecond: 100, gracePeriod: TimeSpan.FromSeconds(10));

                })
                .Build();
    }
   // public class Program
   // {
   //     public static void Main(string[] args)
   //     {
   //         CreateHostBuilder(args).Build().Run();
   //     }
   //
   //     public static IHostBuilder CreateHostBuilder(string[] args) =>
   //         Host.CreateDefaultBuilder(args)
   //             .ConfigureWebHostDefaults(webBuilder =>
   //             {
   //                 webBuilder.UseStartup<Startup>();
   //             });
   // }
}
