using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;
using WCMS.Framework.Core.Manager;

namespace WCMS.Framework
{
    public class WebUserGroup : IWebObject, ISelfManager
    {
        private static IWebUserGroupProvider _manager;

        static WebUserGroup()
        {
            _manager = WebObject.ResolveManager<WebUserGroup, IWebUserGroupProvider>(WebObject.ResolveProvider<WebUserGroup, IWebUserGroupProvider>());
        }

        public WebUserGroup()
        {
            Id = -1;
            //UserId = -1;
            GroupId = -1;
            ObjectId = -1;
            RecordId = -1;
            CreatedById = -1;

            Active = 1;
            DateJoined = DateTime.Now;
            Remarks = string.Empty;
        }

        #region Properties

        [ObjectColumn(true)]
        public int Id { get; set; }

        //[ObjectColumn]
        //public int UserId { get; set; }

        [ObjectColumn]
        public int GroupId { get; set; }

        [ObjectColumn]
        public int Active { get; set; }

        [ObjectColumn]
        public DateTime DateJoined { get; set; }

        [ObjectColumn]
        public int ObjectId { get; set; }

        [ObjectColumn]
        public int RecordId { get; set; }

        [ObjectColumn]
        public string Remarks { get; set; }

        public int CreatedById { get; set; }

        public WebUser CreatedBy
        {
            get
            {
                if (CreatedById > 0)
                    return WebUser.Get(CreatedById);
                return null;
            }
        }

        public WebUser User { get { return WebUser.Get(this.RecordId); } }
        public bool IsActive { get { return Active == 1; } set { Active = value ? 1 : 0; } }
        public int UserId { get { return RecordId; } set { RecordId = value; } }

        public WebGroup Group
        {
            get
            {
                return WebGroup.Get(this.GroupId);
            }
        }

        #endregion

        public int Update()
        {
            return _manager.Update(this);
        }

        #region Static Methods

        public static WebUserGroup Get(int id)
        {
            return _manager.Get(id);
        }

        public static WebUserGroup Get(int groupId, int userId)
        {
            return _manager.Get(groupId, userId);
        }

        public static IEnumerable<WebUserGroup> GetList()
        {
            return _manager.GetList();
        }

        public static IEnumerable<WebUserGroup> GetByUserId(int userId, int active = -1)
        {
            return _manager.GetByUserId(userId, active);
        }

        public static IEnumerable<WebUserGroup> GetByGroupId(int groupId, int active = -1)
        {
            return _manager.GetByGroupId(groupId, active);
        }

        public static bool Delete(int id)
        {
            return _manager.Delete(id);
        }

        public static bool Delete(int userId, int groupId)
        {
            return _manager.Delete(userId, groupId);
        }

        public static IWebUserGroupProvider Provider
        {
            get { return _manager; }
        }

        #endregion

        #region IWebObject Members

        public int OBJECT_ID
        {
            get { return WebObjects.WebUserGroup; }
        }

        #endregion

        #region ISelfManager Members

        public bool Delete()
        {
            return _manager.Delete(this.Id);
        }

        #endregion
    }
}
