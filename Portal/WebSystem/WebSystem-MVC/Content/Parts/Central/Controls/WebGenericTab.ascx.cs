using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;
using WCMS.WebSystem.Controls;

using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.WebSystem.ViewModel;


namespace WCMS.WebSystem.WebParts.Central.Controls
{
    public partial class WebGenericTab : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                var query = new WQuery(this);
                if (!IsPostBack)
                {
                    int pageId = query.GetId(WebColumns.PageId);
                    int siteId = query.GetId(WebColumns.SiteId);
                    int elementId = query.GetId(WebColumns.PageElementId);
                    int masterId = query.GetId(WebColumns.MasterPageId);
                    int parameterSetId = query.GetId(WebColumns.ParameterSetId);

                    var partId = query.GetId(WebColumns.PartId);
                    var controlId = query.GetId(WebColumns.PartControlId);
                    var adminId = query.GetId(WebColumns.PartAdminId);

                    query.Remove(WConstants.Load);
                    if (elementId > 0)
                    {
                        GenericTab.ThemeName = "yellow";
                        DisplayPageName(pageId);

                        var item = WebPageElement.Get(elementId);
                        if (item != null)
                        {
                            if (string.IsNullOrEmpty(Title))
                                Title = item.Name;
                            else
                                Title = string.Format("{0} {1} {2}", Title, WConstants.Arrow, item.Name);
                        }

                        WebPageElementViewModel.BuildTabs(item, query, GenericTab);
                    }
                    else if (masterId > 0)
                    {
                        if (pageId > 0)
                            DisplayPageName(pageId);
                        else
                            linkHeader.HRef = Request.RawUrl;
                        var item = WebMasterPage.Get(masterId);
                        if (item != null)
                        {
                            if (string.IsNullOrEmpty(Title))
                                Title = item.Name;
                            else
                                Title = string.Format("{0} {1} {2}", Title, WConstants.Arrow, item.Name);
                        }

                        WebMasterPageViewModel.BuildTabs(masterId, query, GenericTab);
                    }
                    else if (pageId > 0)
                    {
                        GenericTab.ThemeName = "yellow";
                        var page = DisplayPageName(pageId);
                        WebPageViewModel.BuildTabs(page, query, GenericTab);
                    }
                    else if (siteId > 0)
                    {
                        var site = WSite.Get(siteId);
                        if (site != null)
                        {
                            linkHeader.InnerHtml = site.Name;
                            linkHeader.HRef = site.BuildRelativeUrl(); string.Format("<a href=\"{0}\">{0}</a>", site.BuildRelativeUrl());
                            //panelSiteInfo.Visible = true;
                        }
                        else
                        {
                            linkHeader.InnerHtml = WebRegistry.SelectNode("/System/Title").Value;
                        }

                        WebSiteViewModel.BuildTabs(siteId, query, GenericTab);
                    }
                    else if (parameterSetId > 0)
                    {
                        var q = query.Clone();
                        query.Remove(ObjectKey.KeySource);
                        query.Remove(ObjectKey.KeyString);

                        GenericTab.AddTab("tabParameterSet", "General", query.BuildQuery(CentralPages.WebParameterSetHome), CentralPages.WebParameterSetHome);

                        // ObjectKey
                        q.Set(ObjectKey.KeyString, new ObjectKey(WebObjects.WebParameterSet, parameterSetId));

                        var fileName = query.BasePath;
                        if (!DataHelper.IsPresent(fileName, new string[] { CentralPages.WebParameters }))
                            q.Set(ObjectKey.KeySource, query.EncodedBasePath);

                        GenericTab.AddTab("tabParameters", "Parameters", q.BuildQuery(CentralPages.WebParameters), CentralPages.WebParameters);

                        var item = WebParameterSet.Provider.Get(parameterSetId);
                        if (item != null)
                            Title = item.Name;
                    }
                    else
                    {
                        GenericTab.Visible = false;
                        linkHeader.HRef = Request.RawUrl;

                        if (adminId > 0)
                            linkHeader.InnerHtml = WebPartAdmin.Get(adminId).Name;
                        else if (controlId > 0)
                            linkHeader.InnerHtml = WebPartControl.Get(controlId).Name;
                        else if (partId > 0)
                            linkHeader.InnerHtml = WPart.Get(partId).Name;
                    }
                }

                if (GenericTab.Visible)
                    GenericTab.SelectTab(query.BasePath);
            }
        }

        private WPage DisplayPageName(int pageId)
        {
            WPage page = null;
            if (pageId > 0 && (page = WPage.Get(pageId)) != null)
            {
                Title = page.Name;
                linkHeader.HRef = page.BuildRelativeUrl(); // string.Format("<a href=\"{0}\">{0}</a>", page.BuildRelativeUrl());
                //panelSiteInfo.Visible = true;
            }

            return page;
        }

        private string _title;
        public string Title
        {
            get { return _title; }

            set
            {
                _title = value;
                linkHeader.InnerHtml = _title;
            }
        }

        private string _url;
        public string URL
        {
            get { return _url; }
            set
            {
                _url = value;
                linkHeader.HRef = _url;
            }
        }
    }
}