using System;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

using WCMS.Common.Utilities;
using WCMS.Framework.Core;

namespace WCMS.Framework
{
    public class WPart : SecurableObject
    {
        private static IWebPartProvider _manager;

        static WPart()
        {
            _manager = WebObject.ResolveManager<WPart, IWebPartProvider>(WebObject.ResolveProvider<WPart, IWebPartProvider>());
        }

        public WPart()
        {
            Id = -1;
        }

        [ObjectColumn]
        public string Identity { get; set; }

        [ObjectColumn]
        public int Active { get; set; }

        public int Update()
        {
            return _manager.Update(this);
        }

        public bool Delete()
        {
            return _manager.Delete(this.Id);
        }

        /// <summary>
        /// A method for locating the requested template specified in the "Open"
        /// query key
        /// </summary>
        /// <param name="identityPath">Example: Open=Article/ShortList</param>
        /// <returns></returns>
        public WebPartControlTemplate GetTemplateFromIdentity(string identityPath)
        {
            if (!string.IsNullOrEmpty(identityPath))
            {
                if (identityPath.Contains("/"))
                {
                    // template identity is present
                    string[] identities = identityPath.Split('/');
                    string controlIdentity = identities.First();
                    string templateIdentity = identities[1];

                    WebPartControl control = WebPartControl.Get(Id, controlIdentity);
                    if (control != null)
                        return WebPartControlTemplate.Get(control.Id, templateIdentity);
                }
                else
                {
                    // only Control's identity is present
                    WebPartControl control = WebPartControl.Get(Id, identityPath);
                    if (control != null)
                        return control.Templates.First();
                }
            }

            return null;
        }

        /// <summary>
        /// A method for locating the requested template specified in the "Open"
        /// query key. It will base the search from the supplied sourceTemplateId
        /// </summary>
        /// <param name="sourceTemplateId"></param>
        /// <param name="identityPath"></param>
        /// <returns></returns>
        public WebPartControlTemplate GetTemplateFromIdentity(int sourceTemplateId, string identityPath)
        {
            WebPartControlTemplate template = null;

            if (identityPath.Contains("/"))
            {
                // if template identity is specific then sourceTemplateId can be ignored and identity should be sufficient
                template = GetTemplateFromIdentity(identityPath);
                if (template != null)
                    return template;
            }

            // Locate the template based on the sourceTemplate by assuming that 
            // the template's identity to find is the same with the source
            WebPartControlTemplate sourceTemplate = WebPartControlTemplate.Get(sourceTemplateId);
            WebPartControl control = WebPartControl.Get(Id, identityPath);

            if (control != null)
            {
                template = WebPartControlTemplate.Get(control.Id, sourceTemplate.Identity);

                // If there is template with the identity then just select the first item
                if (template == null)
                    template = control.Templates.First();
            }

            //else if ((control = WebPartControl.Get(Id, sourceTemplate.PartControl.Identity)) != null)
            //{
            //    template = WebPartControlTemplate.Get(control.Id, identityPath);

            //    // If there is template with the identity then just select the first item
            //    if (template == null)
            //        template = control.Templates.First();
            //}

            return template;
        }

        public IEnumerable<WebPartControl> Controls { get { return WebPartControl.GetList(Id); } }

        public IEnumerable<WebPartAdmin> AdminControls { get { return WebPartAdmin.GetList(Id); } }

        public bool IsActive { get { return Active == 1; } }

        #region Static Methods

        public static IWebPartProvider Provider { get { return _manager; } }

        public static explicit operator WPart(DbDataReader r)
        {
            WPart item = new WPart();
            item.Id = DataHelper.GetId(r["PartId"].ToString());
            item.Name = r["Name"].ToString();
            item.Identity = r["Identity"].ToString();
            item.Active = Convert.ToInt32(r["Active"].ToString());

            return item;
        }

        public static WPart Get(int partId)
        {
            return _manager.Get(partId);
        }

        public static WPart Get(string identity)
        {
            return _manager.Get(identity);
        }

        public static IEnumerable<WPart> GetList()
        {
            return _manager.GetList();
        }

        public static bool Delete(int partId)
        {
            return _manager.Delete(partId);
        }

        #endregion

        #region Business Login Methods

        public static List<WPart> GetPermissibleList(int userId)
        {
            var user = WebUser.Get(userId);
            var items = _manager.GetList();

            return (from i in items
                    where WebObjectSecurity.IsUserAdded(i) //(i.GetObjectSecurities().FirstOrDefault(o => user.IsMemberOrEqual(o.SecurityObjectId, o.SecurityRecordId)) != null)
                    select i).ToList();
        }

        #endregion

        public override int OBJECT_ID
        {
            get { return WebObjects.WebPart; }
        }
    }
}
