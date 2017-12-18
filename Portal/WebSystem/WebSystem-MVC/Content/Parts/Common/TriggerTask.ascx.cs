using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.WebSystem.Agent;

namespace WCMS.WebSystem.Content.Parts.Common
{
    public partial class TriggerTask : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                WContext context = new WContext(this);
                var element = context.ParameterizedObject;

                var name = element.GetParameterValue("TaskName");
                if (!string.IsNullOrEmpty(name))
                {
                    var force = DataHelper.GetBool(element.GetParameterValue("Force"), true);

                    var job = WebJob.Provider.Get(name);
                    if (job.ExecutionStatus != ExecutionStatus.Running)
                    {
                        AgentHelper.ExecuteTask(job, force);

                        lblStatus.InnerHtml = string.Format("{0} task executed.", name);
                    }
                    else
                    {
                        lblStatus.InnerHtml = string.Format("{0} task is already running.", name);
                    }
                }
                else
                {
                    lblStatus.InnerHtml = string.Format("Unable to find {0} task.", name);
                }

                linkRefresh.HRef = context.BuildQuery();
            }
        }
    }
}