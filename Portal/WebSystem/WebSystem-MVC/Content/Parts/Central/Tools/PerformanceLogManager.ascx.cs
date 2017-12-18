using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using WCMS.Common;
using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Diagnostics;

namespace WCMS.WebSystem.WebParts.Central.Tools
{
    public partial class PerformanceLogManagerView : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                cmdToggle.Text = WConfig.EnablePerfLogging ? "Debug Off" : "Debug On";
            }
        }


        public DataSet Select()
        {
            WPage page = null;
            var cache = PerformanceLog.Cache;

            return DataHelper.ToDataSet(
                from i in cache
                where ((page = i.PageId > 0 ? WPage.Get(i.PageId) : null) == null || true)
                select new
                {
                    i.Id,
                    i.Content,
                    i.LogDateTime,
                    i.PageId,
                    i.Duration,
                    PageUrl = page == null ? "#" : page.BuildRelativeUrl(),
                    PageName = page == null ? "" : page.Name
                }
            );
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void cmdClear_Click(object sender, EventArgs e)
        {
            PerformanceLog.Cache.Clear();

            GridView1.DataBind();
        }

        protected void cmdDownload_Click(object sender, EventArgs e)
        {
            WPage page = null;
            var cache = PerformanceLog.Cache;

            var ds = DataHelper.ToDataSet(
                from i in cache
                where ((page = i.PageId > 0 ? WPage.Get(i.PageId) : null) == null || true)
                select new
                {
                    i.Id,
                    i.Content,
                    i.LogDateTime,
                    i.PageId,
                    i.Duration,
                    PageUrl = page == null ? "#" : page.BuildRelativeUrl(),
                    PageName = page == null ? "" : page.Name
                }
            );

            WebHelper.DownloadAsXml(ds);
        }

        protected void cmdToggle_Click(object sender, EventArgs e)
        {
            WConfig.EnablePerfLogging = !WConfig.EnablePerfLogging;

            if (!WConfig.EnablePerfLogging)
                PerformanceLog.Cache.Clear();

            QueryParser query = new QueryParser(this);
            query.Redirect();
        }

        protected void cmdAddSummary_Click(object sender, EventArgs e)
        {
            var cache = PerformanceLog.Cache;

            // Add Summary
            TimeSpan total = new TimeSpan();

            cache.ForEach((i) => { total += i.Duration; });

            cache.Add(new PerformanceLog("AVERAGE", new TimeSpan(0, 0, 0, 0, (int)(total.TotalMilliseconds / cache.Count)), -1));
            cache.Add(new PerformanceLog("TOTAL", total, -1));

            GridView1.DataBind();
        }
    }
}
