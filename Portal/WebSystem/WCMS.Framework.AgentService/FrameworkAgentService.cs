using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Hosting;

using WCMS.Common;
using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Agent;

namespace WCMS.Framework.AgentService
{
    public class FrameworkAgentService : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            FrameworkAgent agent = new FrameworkAgent();
            agent.InternalStart(Array.Empty<string>());
            return Task.CompletedTask;
        }
    }
}
