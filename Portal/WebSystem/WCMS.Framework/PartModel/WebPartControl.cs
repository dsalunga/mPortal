using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;

using WCMS.Common.Utilities;
using WCMS.Framework.Core;

namespace WCMS.Framework
{
    public class WebPartControl : SecurableObject, ISelfManager
    {
        private static IWebPartControlProvider _manager;

        static WebPartControl()
        {
            _manager = WebObject.ResolveManager<WebPartControl, IWebPartControlProvider>(WebObject.ResolveProvider<WebPartControl, IWebPartControlProvider>());
        }

        public WebPartControl()
        {
            Id = -1;
            PartId = -1;
            PartAdminId = -1;
            ParentId = -1;

            ConfigFileName = string.Empty;
        }

        public WebPartControl(WPart part)
            : this()
        {
            PartId = part.Id;
        }

        [ObjectColumn]
        public int PartId { get; set; }

        [ObjectColumn]
        public int PartAdminId { get; set; }

        [ObjectColumn]
        public string Identity { get; set; }

        [ObjectColumn]
        public string ConfigFileName { get; set; }

        [ObjectColumn]
        public int EntryPoint { get; set; }

        public int ParentId { get; set; }

        public string GetAdminFile(WebPartAdmin admin = null)
        {
            if (PartAdminId > 0)
            {
                admin = admin ?? PartAdmin;
                if (admin != null)
                    return admin.FileName;
            }

            return ConfigFileName;
        }

        public WebPartAdmin PartAdmin
        {
            get { return PartAdminId > 0 ? WebPartAdmin.Get(PartAdminId) : null; }
        }

        public WPart Part
        {
            get { return WPart.Get(PartId); }
        }

        public int Update()
        {
            return _manager.Update(this);
        }

        public bool Delete()
        {
            this.DeleteRelatedObjects();
            this.DeleteSecurityObjects();
            var templates = this.Templates.ToList();
            for (int i = 0; i < templates.Count(); i++)
            {
                var template = templates[i];
                template.DeleteRelatedObjects();
                template.DeleteSecurityObjects();
                template.Delete();
            }

            return _manager.Delete(this.Id);
        }

        public IEnumerable<WebPartControlTemplate> Templates
        {
            get { return WebPartControlTemplate.GetList(Id); }
        }

        public static explicit operator WebPartControl(DbDataReader r)
        {
            var item = new WebPartControl();
            item.Id = DataHelper.GetId(r["PartControlId"].ToString());
            item.PartId = DataHelper.GetId(r["PartId"].ToString());
            item.Name = DataHelper.Get(r, WebColumns.Name);
            item.Identity = r["Identity"].ToString();
            item.ConfigFileName = r["ConfigFileName"].ToString();
            item.PartAdminId = DataHelper.GetId(r, WebColumns.PartAdminId);
            item.EntryPoint = DataHelper.GetInt32(r, "EntryPoint");
            item.ParentId = DataHelper.GetId(r, WebColumns.ParentId);

            return item;
        }

        //public static explicit operator WebPartControl(DataRow r)
        //{
        //    WebPartControl item = new WebPartControl();
        //    item.Id = DataHelper.GetId(r["PartControlId"].ToString());
        //    item.PartId = DataHelper.GetId(r["PartId"].ToString());
        //    item.Name = r["Name"].ToString();
        //    item.Identity = r["Identity"].ToString();
        //    item.ConfigFileName = r["ConfigFileName"].ToString();

        //    return item;
        //}

        public static WebPartControl Get(int partControlId)
        {
            return _manager.Get(partControlId);
        }

        public static WebPartControl Get(int partId, string identity)
        {
            return _manager.Get(partId, identity);
        }

        public static IEnumerable<WebPartControl> GetList(int partId)
        {
            return _manager.GetList(partId);
        }

        public static bool Delete(int partControlId)
        {
            var control = WebPartControl.Get(partControlId);
            if (control != null)
                return control.Delete();

            return false;
        }

        public override int OBJECT_ID
        {
            get { return WebObjects.WebPartControl; }
        }

        public bool IsEntryPoint
        {
            get { return EntryPoint == 1; }
            set { EntryPoint = value ? 1 : 0; }
        }
    }
}
