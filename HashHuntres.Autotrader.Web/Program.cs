using HashHunters.Autotrader;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;

namespace HashHuntres.Autotrader.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var repository = new OHLCRedisRepository();
            repository.WriteTicker("asc", DateTime.Now, 12);
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
