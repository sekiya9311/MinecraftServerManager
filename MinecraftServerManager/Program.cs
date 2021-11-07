using System;
using System.Threading.Tasks;
using ConsoleAppFramework;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MinecraftServerManager.Commands;
using MinecraftServerManager.Services;

namespace MinecraftServerManager
{
    class Program : ConsoleAppBase
    {
        static async Task Main(string[] args)
        {
            await Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services
                        .AddSingleton<StartupCommand>()
                        .AddSingleton<DailyRefreshCommand>()
                        .AddSingleton<ShutdownCommand>()
                        .AddSingleton<MinecraftClient>()
                        .AddSingleton<DiscordBotClient>()
                        .AddSingleton<DropboxClient>()
                        .Configure<AppConfig>(context.Configuration);
                })
                .RunConsoleAppFrameworkAsync<Program>(args);
        }

        public void Hello() => Console.WriteLine("Hello 👋");
        
        [Command("startup", "Start Bedrock process.")]
        public void Startup() => Context.ServiceProvider.GetService<StartupCommand>().Run();

        [Command("refresh", "Refresh Bedrock process and backup.")]
        public void DailyRefresh() => Context.ServiceProvider.GetService<DailyRefreshCommand>().Run();

        [Command("shutdown", "Shutdown Bedrock process.")]
        public void Shutdown() => Context.ServiceProvider.GetService<ShutdownCommand>().Run();
    }
}
