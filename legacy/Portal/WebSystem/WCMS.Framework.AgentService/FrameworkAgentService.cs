using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;

using System.Text;
using System.Threading;
using System.IO;

using WCMS.Common;
using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Agent;

namespace WCMS.Framework.AgentService
{
    public partial class FrameworkAgentService : ServiceBase
    {
        public FrameworkAgentService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            FrameworkAgent agent = new FrameworkAgent();
            agent.InternalStart(args);
        }

        protected override void OnStop()
        {
            
        }

        protected override bool OnPowerEvent(PowerBroadcastStatus powerStatus)
        {
            return base.OnPowerEvent(powerStatus);
        }

        protected override void OnShutdown()
        {
            base.OnShutdown();
        }

        
    }
}
