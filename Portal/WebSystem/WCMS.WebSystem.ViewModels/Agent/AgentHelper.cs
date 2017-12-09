using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.Agent
{
    public abstract class AgentHelper
    {
        public static bool ExecuteTask(int id, bool forceExecute = true)
        {
            if (id > 0)
            {
                var job = WebJob.Provider.Get(id);
                ExecuteTask(job, forceExecute);
            }
            else
            {
                // "Invalid Job ID.";
            }

            return false;
        }

        public static void ExecuteAutoStart()
        {
            try
            {
                var processes = Process.GetProcessesByName(AgentConfig.ProcessName);
                if (processes.Length == 0)
                {
                    var processPath = WebHelper.MapPath(AgentConfig.ExecutablePath);
                    var m = new Process();
                    m.StartInfo.Arguments = "/interactive";
                    m.StartInfo.FileName = processPath;
                    m.StartInfo.WorkingDirectory = FileHelper.GetFolder(processPath, '\\');
                    m.Start();
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
            }
        }

        public static bool ExecuteTask(string taskName, bool forceExecute = true)
        {
            if (!string.IsNullOrEmpty(taskName))
            {
                var job = WebJob.Provider.Get(taskName);
                ExecuteTask(job, forceExecute);
            }
            else
            {
                // "Invalid Job Name.";
            }

            return false;
        }

        public static bool ExecuteTask(WebJob job, bool forceExecute = true)
        {
            try
            {
                if (forceExecute || (job != null && job.IsEnabled && job.ExecutionStatus != ExecutionStatus.Running))
                {
                    var processPath = WebHelper.MapPath(AgentConfig.ExecutablePath, true);

                    Process m = new Process();
                    m.StartInfo.Arguments = string.Format(forceExecute ? "/task:{0} /force" : "/task:{0}", job.Name);
                    m.StartInfo.FileName = processPath;
                    m.StartInfo.WorkingDirectory = FileHelper.GetFolder(processPath, '\\');
                    m.Start();

                    return true;
                }
                else
                {
                    // "Job is already running.";
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                throw ex;
            }

            return false;
        }
    }
}
