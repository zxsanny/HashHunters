using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace HashHuntres.Autotrader.Web
{
    public class Program
    {
        public static void Main(string[] args) => 
            WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>().Build().Run();
    }
}
