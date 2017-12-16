using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Data;

using WCMS.Common.Utilities;

namespace WCMS.WebSystem.WebParts.Jobs
{
    public partial class JobSearch : System.Web.UI.UserControl
    {
        #region Constant Members
        private const string publisher = "3462123152151013";
        private const int pageSize = 10;
        #endregion Constant Members

        #region Page Event Handler
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCountries();
            }
        }
        #endregion Page Load Event Handler

        #region Control Event Handlers
        protected void btnFindJobs_Click(object sender, EventArgs e)
        {
            LoadJobSearchResults(1);
        }
        protected void Page_Changed(object sender, EventArgs e)
        {
            int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
            LoadJobSearchResults(pageIndex);
        }
        #endregion Control Event Handlers

        #region Helper Methods
        private void LoadCountries()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("~/Content/Parts/Jobs/Xml/countries.xml"));

            foreach (XmlNode node in doc.SelectNodes("//country"))
            {
                ddlCountry.Items.Add(new ListItem(node.InnerText, node.Attributes["code"].InnerText));
            }

            ListItem clientCountry = ddlCountry.Items.FindByText(System.Globalization.RegionInfo.CurrentRegion.DisplayName);
            if (clientCountry != null)
                ddlCountry.SelectedIndex = ddlCountry.Items.IndexOf(clientCountry);

        }
        private void LoadJobSearchResults(int pageNo)
        {
            xdsJobSearchResult.Data = GetJobSearchResultXml(pageNo);
            xdsJobSearchResult.XPath = "response/results/result";
            xdsJobSearchResult.DataBind();

            lvJobSearchResults.Items.Clear();
            lvJobSearchResults.DataSource = xdsJobSearchResult;
            lvJobSearchResults.DataBind();

            if (lvJobSearchResults.Items.Count > 0)
            {
                PopulatePager(int.Parse(xdsJobSearchResult.GetXmlDocument().SelectSingleNode("response/totalresults").InnerText)
                    , pageNo, 19);
            }
            else
            {
                rptPager.DataSource = null;
                rptPager.DataBind();
            }

            Label lblNoJobsSearch = lvJobSearchResults.FindControl("lblNoJobsSearch") as Label;
            if (lblNoJobsSearch != null)
            {
                lblNoJobsSearch.Text = "Jobs "
                    + xdsJobSearchResult.GetXmlDocument().SelectSingleNode("response/start").InnerText
                    + " to "
                    + xdsJobSearchResult.GetXmlDocument().SelectSingleNode("response/end").InnerText
                    + " of "
                    + xdsJobSearchResult.GetXmlDocument().SelectSingleNode("response/totalresults").InnerText;
            }
        }
        private string GetJobSearchResultXml(int pageNo)
        {
            string url = string.Format(
                "http://api.indeed.com/ads/apisearch?publisher={0}&q={1}&l={2}&sort=&radius=&st=&jt=&start={3}&limit=&fromage=&filter=&latlong=1&co={4}&chnl=&userip={5}&useragent={6}&v=2"
                , publisher, txtWhat.Text.Trim(), txtWhere.Text.Trim(), (pageNo * pageSize) - pageSize, ddlCountry.SelectedValue.ToLower(), Request.UserHostAddress, Request.UserAgent);

            WebClient webClient = new WebClient();
            string jobSearchXmlFeed = webClient.DownloadString(url);
            return jobSearchXmlFeed;
        }
        private void PopulatePager(int totalRecords, int currentPageNo, int totalNumericLinks)
        {
            if (totalRecords <= pageSize)
            {
                rptPager.DataSource = null;
                rptPager.DataBind();
                return;
            }

            int totalPages = totalRecords / pageSize + (totalRecords % pageSize > 0 ? 1 : 0);
            int startPageNo, endPageNo;

            if (totalNumericLinks >= totalPages)
            {
                startPageNo = 1;
                endPageNo = totalPages;
            }
            else
            {
                if ((currentPageNo - (totalNumericLinks / 2)) > 0)
                {
                    endPageNo = ((currentPageNo + (totalNumericLinks / 2)) - (totalNumericLinks - 1) % 2);

                    if (endPageNo > totalPages)
                        endPageNo = totalPages;
                }
                else
                    endPageNo = totalNumericLinks;

                startPageNo = (endPageNo - totalNumericLinks) + 1 > 0 ? (endPageNo - totalNumericLinks) + 1 : 1;
            }

            List<ListItem> pages = new List<ListItem>();

            pages.Add(new ListItem("Previous", (currentPageNo - 1).ToString(), currentPageNo > 1));
            for (int i = startPageNo; i <= endPageNo; i++)
            {
                pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPageNo));
            }
            pages.Add(new ListItem("Next", (currentPageNo + 1).ToString(), currentPageNo < totalPages));

            rptPager.DataSource = pages;
            rptPager.DataBind();
        }
        #endregion Helper Methods

        public DataSet Select()
        {
            return DataUtil.ToDataSet(
                from i in Job.Provider.GetList()
                select i
            );
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        //protected void lbtnJobTitle_OnClick(object sender, EventArgs e)
        //{
        //    LinkButton lbtnJobTitle = sender as LinkButton;
        //    Uri uri = new Uri(lbtnJobTitle.CommandArgument);
        //    string jobkey = System.Web.HttpUtility.ParseQueryString(uri.Query).Get("jk");

        //    string url = string.Format(
        //        "http://api.indeed.com/ads/apigetjobs?publisher={0}&jobkeys={1}&v=2"
        //    , publisher, jobkey);

        //    WebClient webClient = new WebClient();
        //    string jobSearchXmlFeed = webClient.DownloadString(url);

        //    XmlDocument xmlDoc = new XmlDocument();
        //    xmlDoc.LoadXml(jobSearchXmlFeed);

        //    HyperLink hlink = (sender as LinkButton).Parent.FindControl("lnkJobTitle") as HyperLink;

        //    if (hlink != null)
        //    {
        //        Page.ClientScript.RegisterStartupScript(
        //          Page.GetType(),
        //          "clickLink",
        //          "document.getElementById('" + hlink.ClientID + "').click();",
        //          true);
        //    }
        //}
    }
}