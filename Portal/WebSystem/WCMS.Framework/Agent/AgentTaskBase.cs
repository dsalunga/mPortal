using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using WCMS.Common;
using WCMS.Common.Utilities;
using WCMS.Framework.Core;

namespace WCMS.Framework
{
    public abstract class AgentTaskBase
    {
        protected AgentTaskBase()
        {
            Attributes = new Dictionary<string, string>();
            Logger = new LogManager();
        }

        public void StartManagedExecution()
        {
            if (!ForcedExecution && !Job.IsEnabled)
            {
                WriteLog("[{0}] {1} Job is disabled.", TaskName, DateTime.Now);
                return;
            }

            // Check execution schedule here

            WriteLog("[{0}] {1} Job execution started...", TaskName, DateTime.Now);


            Job.ExecutionStatus = Framework.ExecutionStatus.Running;
            Job.ExecutionStartDate = DateTime.Now;
            Job.Update();

            // Execute the job
            try
            {
                Execute();

                Job.ExecutionMessage = string.Empty;
            }
            catch (ThreadAbortException ex)
            {
                Job.ExecutionMessage = ex.Message;
                //CompleteExecution();
            }
            catch (Exception ex)
            {
                //LogHelper.WriteLog(ex);
                WriteLog("[{0}] {1} Error encountered: {2}", TaskName, DateTime.Now, ex);

                Job.ExecutionMessage = ex.Message;
            }
            finally
            {
                CompleteExecution();
            }

            WriteLog("[{0}] {1} Job execution completed.", TaskName, DateTime.Now);
        }

        private void CompleteExecution()
        {
            Job.ExecutionEndDate = DateTime.Now;
            Job.ExecutionStatus = Framework.ExecutionStatus.Completed;
            Job.Update();
        }

        public abstract void Execute();

        public WebJob Job { get; set; }
        public string TaskName { get; set; }
        public LogManager Logger { get; set; }
        public bool ForcedExecution { get; set; }

        public Dictionary<string, string> Attributes { get; set; }

        public void WriteLog(string format, params object[] arg)
        {
            this.Logger.WriteLine(format, arg);
        }

        #region Temporary Properties - will be migrated later on to WebJob

        public int Weekdays { get; set; }

        /// <summary>
        /// Please refer to WCMS.Framework.RecurrenceType enum
        /// </summary>
        public int RecurrenceId { get; set; }

        public int OccursEvery { get; set; }

        public DateTime ExecutionStartDate { get; set; }
        public DateTime ExecutionEndDate { get; set; }

        /// <summary>
        /// Refer to WCMS.Framework.ExecutionStatus
        /// </summary>
        public int ExecutionStatus { get; set; }
        public string ExecutionMessage { get; set; }

        #endregion
    }
}
