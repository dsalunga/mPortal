using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using WCMS.Common;
using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Utilities;
using WCMS.WebSystem.Apps.Integration;

namespace WCMS.WebSystem.Apps.Integration
{
    /// <summary>
    /// Summary description for ODKPrintPreview
    /// </summary>
    public class ODKPrintPreview : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";

            int id = DataUtil.GetId(context.Request, "Id");
            int pageId = DataUtil.GetId(context.Request, "PageId");

            var page = WPage.Get(pageId);
            var item = MemberVisit.Provider.Get(id);

            if (item != null && page != null)
            {
                // Prepare Email
                NamedValueProvider values = new NamedValueProvider();
                values.Add("DateVisited", string.Format("{0:dd-MMMM-yyyy}", item.DateVisited));
                values.Add("Name", item.Name);
                values.Add("Address", item.Address);

                values.Add("MembershipDate", string.Format("{0:dd-MMMM-yyyy}", item.MembershipDate));
                values.Add("TimesVisited", item.TimesVisited);
                values.Add("ContactNo", item.ContactNo);

                values.Add("CaseStatus", item.Status);
                values.Add("CouncillorObservation", item.ActualReport);
                values.Add("ActionTaken", item.ActionTaken);
                values.Add("ReportedBy", AccountHelper.GetPrefixedName(item.CreatedUser));

                //values.Add("BaseAddress", WConfig.BaseAddress.TrimEnd('/'));
                //values.Add("Permalink", string.Format("{0}?ArticleId={1}", page.GenerateAbsoluteUrl(), item.Id));

                string templatePath = "~/Content/Parts/Registration/Template/ODKPrint.htm";
                string customTemplateFile = page.GetParameterValue(MemberConstants.ODKPrintTemplateFileKey, "");
                if (!string.IsNullOrWhiteSpace(customTemplateFile))
                    templatePath = WebUtil.CombineAddress("~/Content/Parts/Registration/Template/", customTemplateFile);

                string printPreview = FileHelper.ReadFile(context.Server.MapPath(templatePath));
                printPreview = Substituter.Substitute(printPreview, values);

                context.Response.Write(printPreview);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}