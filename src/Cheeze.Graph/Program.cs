using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cheeze.Graph
{
    public class Program
    {
        public static Task Main(string[] args) =>
            CreateWebHostBuilder(args).Build().RunAsync();

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost
                .CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) => config.AddEnvironmentVariables("Cheeze:Graph:"))
                .ConfigureServices(
                    (hostContext, services) =>
                    {
                        services.AddOptions<Store.Configuration>().Bind(hostContext.Configuration.GetSection("Store"));
                        services.AddOptions<Inventory.Configuration>().Bind(hostContext.Configuration.GetSection("Inventory"));
                    })
                .UseStartup<Startup>();
        }
    }
}
