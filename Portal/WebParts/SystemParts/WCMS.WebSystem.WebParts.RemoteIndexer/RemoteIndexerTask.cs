using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Common;
using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.WebSystem.WebParts.RemoteIndexer.Common;

namespace WCMS.WebSystem.WebParts.RemoteIndexer
{
    public class RemoteIndexerTask : AgentTaskBase
    {
        #region IAgentTask Members

        public override void Execute()
        {
            Logger.WriteLine("[{0}] {1} Execution started.", TaskName, DateTime.Now);

            var libraries = RemoteLibrary.Provider.GetList()
                .Where(i=> i.IsActive);

            foreach (var library in libraries)
            {
                try
                {
                    Logger.WriteLine("[{0}] {1} Indexing started for {2}.", TaskName, DateTime.Now, library.Name);

                    library.RunIndexer(TaskName.ToString(), this);
                }
                catch (Exception ex)
                {
                    Logger.WriteLine("[{0}] {1} {2}.", TaskName, DateTime.Now, ex.ToString());
                    Logger.WriteLine();
                }
            }

            Logger.WriteLine("[{0}] {1} Execution completed.", TaskName, DateTime.Now);
        }

        #endregion
    }
}
