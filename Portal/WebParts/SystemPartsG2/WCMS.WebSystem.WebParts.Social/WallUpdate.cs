using System;
using WCMS.Framework.Core;
using WCMS.Framework.Social.Providers;

namespace WCMS.Framework.Social
{
    public class WallUpdate : WebObjectBase, ISelfManager
    {
        private static IWallUpdateProvider _provider;

        static WallUpdate()
        {
            _provider = WebObject.ResolveManager<WallUpdate, IWallUpdateProvider>(WebObject.ResolveProvider<WallUpdate, IWallUpdateProvider>());
        }

        public WallUpdate()
        {
            UpdateObjectId = -1;
            UpdateRecordId = -1;
            ByRecordId = -1;
            ByObjectId = -1;
            EventTypeId = -1;

            UpdateDate = DateTime.Now;
            Content = string.Empty;
        }

        public int UpdateRecordId { get; set; }
        public int UpdateObjectId { get; set; }
        public int ByRecordId { get; set; }
        public int ByObjectId { get; set; }
        public int EventTypeId { get; set; }
        public string Content { get; set; }
        public DateTime UpdateDate { get; set; }

        public override int OBJECT_ID { get { return SocialConstants.WallUpdate; } }

        public static IWallUpdateProvider Provider { get { return _provider; } }

        //public IWallUpdateEvent GenerateEventObject()
        //{
        //    switch (EventTypeId)
        //    {
        //        case WallEventTypes.StatusUpdate:
        //            return new ProfileUpdateEvent(
        //    }

        //    return null;
        //}

        #region ISelfManager Members

        public bool Delete()
        {
            return _provider.Delete(this.Id);
        }

        public int Update()
        {
            return _provider.Update(this);
        }

        #endregion
    }
}
