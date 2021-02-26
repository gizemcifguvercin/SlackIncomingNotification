using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace NotificationConsumer
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var hostBuilder = new HostBuilder()
                .ConfigureHostConfiguration((config) => { config.AddEnvironmentVariables(prefix: "ASPNETCORE_"); })
                .ConfigureAppConfiguration((hostContext, config) =>
                {
                    config.SetBasePath(Environment.CurrentDirectory);
                    config.AddJsonFile("appsettings.json", optional: false);
                    config.AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: true);
                    config.AddEnvironmentVariables();

                    if (args != null)
                        config.AddCommandLine(args);

                    config.Build().BindConfigurations();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.Configure(hostContext.Configuration);
                });

            await hostBuilder.RunConsoleAsync();
        }
    }
}