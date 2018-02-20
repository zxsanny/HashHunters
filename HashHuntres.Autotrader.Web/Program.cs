using Microsoft.AspNetCore.Hosting;

namespace HashHuntres.Autotrader.Web
{
    public class Program
    {
        public static void Main(string[] args) =>
            new WebHostBuilder()
            .UseKestrel()
            .UseStartup<Startup>().Build().Run();
    }
}
