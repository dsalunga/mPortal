using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace WCMS.Framework.Agent
{
    class Program
    {
        static void Main(string[] args)
        {
            FrameworkAgent agent = new FrameworkAgent();
            agent.InternalStart(args);
        }
    }
}
