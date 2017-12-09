using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

using WCMS.Common;
using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.Framework.Agent
{
    public class FrameworkAgent
    {
        private const string TASK_ARG = "/task:";

        /// <summary>
        /// For single-job execution. Ignores all status and force execution.
        /// </summary>
        private const string TASK_FORCE = "/force";

        /// <summary>
        /// Allows user to terminate the Agent thru console.
        /// </summary>
        private const string TASK_INTERACTIVE = "/interactive";

        /// <summary>
        /// Value in seconds
        /// </summary>
        private static int execInterval = 0; // # of AgentTasks

        private static LogManager _logManager;
        private static string _logPath;
        private static bool forceExecute;
        private static bool interactiveExec;

        public void InternalStart(string[] args)
        {
            // Turn off WebRegistry Cache
            var webRegistry = WebObject.Get(WebObjects.WebRegistry);
            if (webRegistry != null)
                webRegistry.CacheTypeId = CacheTypes.None;

            List<Thread> workerThreads = new List<Thread>();

            var agentNode = WebRegistry.SelectNode("/System/Agent");

            var jobCacheRefreshInterval = agentNode.SelectSingleNode("JobCacheRefreshInterval").ValueInt32; // In seconds
            execInterval = agentNode.SelectSingleNode("JobTimerInterval").ValueInt32; // In seconds

            if (args.Length > 0)
            {
                forceExecute = !string.IsNullOrEmpty(args.SingleOrDefault(arg => arg.Equals(TASK_FORCE, StringComparison.InvariantCultureIgnoreCase)));
                interactiveExec = !string.IsNullOrEmpty(args.SingleOrDefault(arg => arg.Equals(TASK_INTERACTIVE, StringComparison.InvariantCultureIgnoreCase)));
            }

            #region Configure Logger

            _logPath = ConfigHelper.Get("LogPath");
            if (string.IsNullOrEmpty(_logPath))
                _logPath = AppDomain.CurrentDomain.BaseDirectory;
            else if (_logPath.StartsWith(@".\"))
                _logPath = AppDomain.CurrentDomain.BaseDirectory + _logPath.Substring(1);

            var logPath = Path.Combine(_logPath, "Agent.txt");

            _logManager = new LogManager(new List<ILogger>
                {
                    new ConsoleLogger(),
                    new FileLogger(logPath)
                }
            );

            #endregion


            _logManager.WriteLine("JobCacheRefreshInterval: {0}, JobTimerInterval: {1}", jobCacheRefreshInterval, execInterval);

            #region Scheduler Thread

            Thread schedulerThread = new Thread(() =>
            {
                string taskNameArg = args.Length > 0 ? args.SingleOrDefault(arg => arg.StartsWith(TASK_ARG, StringComparison.InvariantCultureIgnoreCase)) : string.Empty;
                if (!string.IsNullOrEmpty(taskNameArg))
                {
                    // Execute Single Job
                    string taskName = taskNameArg.Substring(TASK_ARG.Length).Trim();
                    if (!string.IsNullOrWhiteSpace(taskName))
                    {
                        while (true)
                        {
                            var forceExitNode = agentNode.SelectSingleNode("ForceExit");
                            var forceExit = forceExitNode == null ? false : forceExitNode.ValueBool;
                            if (forceExit)
                                break;

                            DateTime lastCacheRefresh = DateTime.Now;

                            var job = WebJob.Provider.Get(taskName);
                            //var regJob = agentNode.SelectSingleNode("Jobs/" + taskName);

                            while (true)
                            {
                                if (forceExecute || job.IsForExecution(DateTime.Now, execInterval)) // Check if job is convered by schedule
                                    workerThreads.Add(PrepareAndExecuteJob(job));

                                if (!forceExecute)
                                {
                                    if (execInterval > 0)
                                        Sleep();
                                    else
                                        break;

                                    // Check for job refresh
                                    if (DateTimeHelper.IsOccurring(DateTime.Now, lastCacheRefresh, jobCacheRefreshInterval))
                                        break;
                                }
                                else
                                {
                                    // Forced Execution, run only once
                                    break;
                                }
                            }

                            if (!(execInterval > 0) || forceExecute)
                                break;
                        }
                    }
                }
                else
                {
                    // Execute All Jobs
                    while (true)
                    {
                        var forceExitNode = agentNode.SelectSingleNode("ForceExit");
                        var forceExit = forceExitNode == null ? false : forceExitNode.ValueBool;
                        if (forceExit)
                            break;

                        DateTime lastCacheRefresh = DateTime.Now;
                        var jobCache = WebJob.Provider.GetList();

                        while (true)
                        {
                            foreach (var job in jobCache)
                                if (job.IsEnabled && job.IsForExecution(DateTime.Now, execInterval)) // Check if job is convered by schedule
                                    workerThreads.Add(PrepareAndExecuteJob(job));

                            if (execInterval > 0)
                                Sleep();
                            else
                                break;

                            // Check for job refresh
                            if (DateTimeHelper.IsOccurring(lastCacheRefresh, DateTime.Now, jobCacheRefreshInterval))
                                break;
                        }

                        if (!(execInterval > 0))
                            break;
                    }
                }
            }
            );

            schedulerThread.IsBackground = false; // Always dependent to job threads // !forceExecute;
            schedulerThread.SetApartmentState(ApartmentState.STA);
            schedulerThread.Start();

            #endregion

            if (!forceExecute && interactiveExec)
            {
                _logManager.WriteLine(agentNode.SelectSingleNode("Name").Value);
                _logManager.WriteLine();
                _logManager.WriteLine("Tasks execution started. Press ENTER to stop.");
                _logManager.WriteLine();

                Console.ReadLine();
                _logManager.WriteLine("Aborting Threads. Press wait...");

                // Abort Scheduler Thread
                schedulerThread.Abort();

                // Abort all job threads
                foreach (var thread in workerThreads)
                    thread.Abort();

                _logManager.WriteLine();
                //Console.WriteLine(totalOrdersProcessed + " Orders processed.");
                _logManager.WriteLine("Processing stopped. Press ENTER to exit.");
                Console.ReadLine();
            }
        }

        private void Sleep()
        {
            //var now = DateTime.Now;
            //_logManager.WriteLine("#{0}# {1} Sleeping for {2} seconds, next check is {3}", "Scheduler", now, execInterval, now.AddSeconds(execInterval));

            Thread.Sleep(execInterval * 1000);
        }

        private Thread PrepareAndExecuteJob(WebJob job)
        {
            var task = ReflectionUtil.LoadAndCreateInstance<AgentTaskBase>(job.TypeName);
            if (task != null)
            {
                task.Job = job;
                task.ForcedExecution = forceExecute;
                task.TaskName = job.Name;

                #region Configure Logger

                var logPath = Path.Combine(_logPath, job.Name + ".txt");

                task.Logger = new LogManager(
                    new List<ILogger>
                    {
                        new ConsoleLogger(),
                        new FileLogger(logPath)
                    }
                );

                #endregion

                // Assign all Attributes
                var parameters = job.Parameters;
                foreach (var parameter in parameters)
                    task.Attributes.Add(parameter.Name, parameter.Value);

                var workTicketThread = new Thread(task.StartManagedExecution);

                // Make this a background thread, so it will terminate when the main thread/process is de-activated
                workTicketThread.IsBackground = false; // Do not terminate process while there's a job thread running // !forceExecute;
                workTicketThread.SetApartmentState(ApartmentState.STA);

                // Start the Work
                workTicketThread.Start();

                return workTicketThread;
            }
            else
            {
                _logManager.WriteLine("[ERROR] Task object is NULL: {0} -- ", job.Name, job.TypeName);

                return null;
            }
        }
    }
}
