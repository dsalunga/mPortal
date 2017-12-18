using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using System.Diagnostics;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.WebSystem.Agent;

namespace WCMS.WebSystem.WebParts.Central.Agent
{
    public partial class Dashboard : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                this.ViewSenderInfo();
        }

        //private void ViewProcesses()
        //{
        //    StringBuilder sb = new StringBuilder();
        //    Process[] processes = Process.GetProcesses();

        //    foreach (Process process in processes)
        //        sb.Append(process.ProcessName + "<br/>");

        //    lProcesses.Text = sb.ToString();
        //}


        private void ViewSenderInfo()
        {
            StringBuilder sb = new StringBuilder();
            Process[] ms = Process.GetProcessesByName(AgentConfig.ProcessName);

            if (ms.Length > 0)
            {
                foreach (Process m in ms)
                {
                    sb.AppendFormat("<strong>Process Name: {0}, ID: {1}</strong><br/>", m.ProcessName, m.Id);
                    sb.AppendFormat("Private Memory used: {0}<br/>", FileHelper.GetSizeString(m.PrivateMemorySize64));
                    sb.AppendFormat("Virtual Memory Used: {0}<br/>", FileHelper.GetSizeString(m.VirtualMemorySize64));
                    sb.AppendFormat("Started at {0}<br/>", m.StartTime.ToString("MMMM dd, yyyy h:mm tt"));
                    sb.AppendFormat("CPU Time: {0}<br/><br/>", m.TotalProcessorTime);

                    // StartInfo
                    //sb.AppendFormat("FileName: {0}<br/>", m.StartInfo.FileName);
                    //sb.AppendFormat("Arguments: {0}<br/>", m.StartInfo.Arguments);
                    //sb.AppendFormat("Working Directory: {0}<br/>", m.StartInfo.WorkingDirectory);
                    sb.Append("<hr/><br/>");
                }
            }
            else
            {
                sb.Append("<span style=\"color: green;\">" + AgentConfig.Name + " not running.</span>");
            }

            lProcesses.Text = sb.ToString();
        }

        protected void cmdRefresh_Click(object sender, EventArgs e)
        {
            this.ViewSenderInfo();
        }

        protected void cmdRun_Click(object sender, EventArgs e)
        {
            var processPath = WebHelper.MapPath(AgentConfig.ExecutablePath);

            //StringBuilder sb = new StringBuilder();
            Process m = new Process();

            m.StartInfo.Arguments = "/interactive";
            //m.StartInfo.RedirectStandardInput = true;
            //m.StartInfo.UseShellExecute = false;
            m.StartInfo.FileName = processPath;
            m.StartInfo.WorkingDirectory = FileHelper.GetFolder(processPath, '\\');
            m.Start();

            //sb.AppendFormat("Started at {0}<br/>", m.StartTime.ToString("MMMM dd, yyyy h:mm tt"));

            //lProcesses.Text = sb.ToString();

            ViewSenderInfo();
        }

        protected void cmdStop_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            Process[] processes = Process.GetProcessesByName(AgentConfig.ProcessName);
            int count = processes.Length;

            foreach (Process process in processes)
            {
                sb.AppendFormat("Attempting to end the process... ({0}, {1})<br/>", process.ProcessName, process.Id);

                sb.Append("Killing process...<br/>");
                process.Kill();

                break;
            }

            if (count > 0)
                PerformTaskReset(sb);
            else
                sb.Append("Nothing to stop.<br/>");

            lProcesses.Text = sb.ToString();
        }

        private static void PerformTaskReset(StringBuilder sb)
        {
            sb.AppendFormat("Stopped at {0}<br/>", DateTime.Now.ToString("MMMM dd, yyyy h:mm tt"));

            sb.Append("<br/><br/>Resetting running job states to Completed...");

            var jobs = WebJob.Provider.GetList();
            foreach (var job in jobs)
            {
                if (job.ExecutionStatus == ExecutionStatus.Running)
                {
                    job.ExecutionStatus = ExecutionStatus.Completed;
                    job.ExecutionEndDate = DateTime.Now;
                    job.Update();
                }
            }

            sb.Append("done.");
        }

        protected void cmdTerminate_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            Process[] processes = Process.GetProcessesByName(AgentConfig.ProcessName);
            int count = processes.Length;

            foreach (Process process in processes)
            {
                sb.AppendFormat("Attempting to end the process... ({0}, {1})<br/>", process.ProcessName, process.Id);

                sb.Append("Killing process...<br/>");
                process.Kill();

                break;
            }

            if (count > 0)
                PerformTaskReset(sb);
            else
                sb.Append("Nothing to stop.<br/>");

            lProcesses.Text = sb.ToString();
        }
    }
}