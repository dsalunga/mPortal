using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web;

using WCMS.Common.Utilities;
using WCMS.Framework.Net;
using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.Handlers
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DataSync" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select DataSync.svc or DataSync.svc.cs at the Solution Explorer and start debugging.
    public class DataSync : IDataSync
    {
        public void DoWork()
        {
        }

        public WebUser GetObject()
        {
            return new WebUser();
        }

        public List<WebUser> GetObjectList(int objectId)
        {
            switch (objectId)
            {
                case WebObjects.WebUser:
                    return WebUser.GetList().ToList<WebUser>();
            }

            return null;
        }

        public List<WebSiteIdentity> GetBindings()
        {
            List<WebSiteIdentity> bindings = new List<WebSiteIdentity>();

            bindings.AddRange(WebSiteIdentity.Provider.GetList());

            return bindings;
        }

        public List<FileSyncInfo> GetFiles(string relativePath = "~", bool recursive = true)
        {
            var items = new List<FileSyncInfo>();

            var rootPath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
            var folder = FileHelper.Combine(rootPath, relativePath.Replace("~", ""), '\\');

            DirectoryInfo rootDir = new DirectoryInfo(folder);

            var files = rootDir.EnumerateFiles();
            foreach(var fileInfo in files)
            {
                var file = new FileSyncInfo();
                file.RelativePath = fileInfo.Name;
                file.DateModified = fileInfo.LastWriteTime;
                file.Size = fileInfo.Length;

                items.Add(file);
            }

            return items;
        }


        public WebUserContainer GetUserComplete(int userId)
        {
            var user = WebUser.Get(userId);
            return user != null ? new WebUserContainer(user, RemoteItemTypes.REMOTE) : null;
        }

        public void SetUserComplete(WebUserContainer container)
        {
            if (container != null && container.User != null 
                && (container.ItemType == RemoteItemTypes.REMOTE || container.ItemType == RemoteItemTypes.IDENTICAL))
            {
                List<WebGroup> existingGroups = new List<WebGroup>();
                List<WebAddress> existingAddresses = new List<WebAddress>();

                var user = WebUser.Get(container.User.UserName);
                if (user == null)
                {
                    user = container.User;
                    user.Id = -1;
                    user.Update();
                }
                else
                {
                    existingAddresses.AddRange(user.Addresses);
                    existingGroups.AddRange(user.Groups);
                }

                foreach (var remoteGroup in container.Groups)
                {
                    if (existingGroups.Find(i => i.Name.Equals(remoteGroup.Name, StringComparison.InvariantCultureIgnoreCase)) == null)
                        user.AddToGroup(remoteGroup.Name);
                }

                foreach (var remoteAddress in container.Addresses)
                {
                    if (existingAddresses.Find(i => i.Tag.Equals(remoteAddress.Tag, StringComparison.InvariantCultureIgnoreCase)) == null)
                    {
                        remoteAddress.Id = -1;
                        remoteAddress.RecordId = user.Id;
                        remoteAddress.Update();
                    }
                }
            }
        }
    }
}
