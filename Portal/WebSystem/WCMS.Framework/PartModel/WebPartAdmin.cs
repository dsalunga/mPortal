using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;

using WCMS.Common.Utilities;
using WCMS.Framework.Core;

namespace WCMS.Framework
{
    public class WebPartAdmin : ParameterizedWebObject
    {
        private static IWebPartAdminProvider _provider = DataAccess.CreateProvider<IWebPartAdminProvider>();

        public WebPartAdmin()
        {
            Id = -1;
            PartId = -1;
            ParentId = -1;
            Active = 1;
            Visible = 1;
            AutoTitle = 1;
            InSiteContext = 0;

            TemplateEngineId = TemplateEngineTypes.ASPX;
        }

        public int PartId { get; set; }
        public string FileName { get; set; }
        public int ParentId { get; set; }
        public int Active { get; set; }
        public int Visible { get; set; }
        public int InSiteContext { get; set; }
        public int AutoTitle { get; set; }
        public int TemplateEngineId { get; set; }

        public bool IsActive
        {
            get { return Active == 1; }
            set { Active = value ? 1 : 0; }
        }

        public bool IsVisible
        {
            get { return Visible == 1; }
            set { Visible = value ? 1 : 0; }
        }

        public bool IsAutoTitle
        {
            get { return AutoTitle == 1; }
            set { AutoTitle = value ? 1 : 0; }
        }

        public bool IsInSiteContext
        {
            get { return InSiteContext == 1; }
            set { InSiteContext = value ? 1 : 0; }
        }

        public WPart Part
        {
            get { return WPart.Get(PartId); }
        }

        public static IWebPartAdminProvider Provider { get { return _provider; } }

        public static string BuildUrl(string partName, string adminName)
        {
            if (!string.IsNullOrEmpty(partName) && !string.IsNullOrEmpty(adminName))
            {
                var part = WPart.Get(partName);
                if (part != null)
                {
                    var admin = _provider.Get(part.Id, adminName);
                    if (admin != null)
                        return WQuery.BuildQuery(admin.TemplateEngineId == TemplateEngineTypes.Razor ? CentralPages.LoaderRazor : CentralPages.LoaderMain, WebColumns.PartAdminId, admin.Id);
                }
            }

            return string.Empty;
        }

        public int Update()
        {
            return _provider.Update(this);
        }

        public static IEnumerable<WebPartAdmin> GetList()
        {
            return _provider.GetList();
        }

        public static IEnumerable<WebPartAdmin> GetList(int partId)
        {
            return _provider.GetList(partId);
        }

        public static IEnumerable<WebPartAdmin> GetList(int partId, int parentId)
        {
            return _provider.GetList(partId, parentId);
        }

        public static WebPartAdmin Get(int partAdminId)
        {
            return _provider.Get(partAdminId);
        }

        public int Update(WebPartAdmin item)
        {
            return _provider.Update(item);
        }

        public static bool Delete(int partAdminId)
        {
            return _provider.Delete(partAdminId);
        }

        public override int OBJECT_ID
        {
            get { return WebObjects.WebPartAdmin; }
        }
    }
}
