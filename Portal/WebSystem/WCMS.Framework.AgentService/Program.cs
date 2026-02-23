using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WCMS.Framework.AgentService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            var builder = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddHostedService<FrameworkAgentService>();
                });

            builder.Build().Run();
        }
    }
}
