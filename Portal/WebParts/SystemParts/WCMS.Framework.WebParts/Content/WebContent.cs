using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;

using WCMS.WebSystem.WebParts.Content.Providers;

namespace WCMS.WebSystem.WebParts.Content
{
    public class WebContent : IWebObject, ISelfManager
    {
        private static IWebContentProvider _manager;

        static WebContent()
        {
            _manager = WebObject.ResolveManager<WebContent, IWebContentProvider>(WebObject.ResolveProvider<WebContent, IWebContentProvider>());
        }

        public WebContent()
        {
            VersionOf = -1;
            Id = -1;
            VersionNo = -1;
            DirectoryId = -1;
            Active = 1;
            SiteId = -1;

            ActiveContent = 0;
        }

        #region Properties

        [ObjectColumn(true)]
        public int Id { get; set; }

        [ObjectColumn]
        public string Title { get; set; }

        [ObjectColumn]
        public string Content { get; set; }

        [ObjectColumn]
        public int VersionOf { get; set; }

        [ObjectColumn]
        public int Active { get; set; }

        [ObjectColumn]
        public int SiteId { get; set; }

        [ObjectColumn]
        public int EditorSensitive { get; set; }

        [ObjectColumn]
        public DateTime DateModified { get; set; }

        [ObjectColumn]
        public int ActiveContent { get; set; }

        public bool IsActive
        {
            get { return Active == 1; }
            set { Active = value ? 1 : 0; }
        }

        public bool IsActiveContent
        {
            get { return ActiveContent == 1; }
            set { ActiveContent = value ? 1 : 0; }
        }

        public WebContent CurrentVersion
        {
            get
            {
                WebContent item = null;

                if (this.VersionOf > 0)
                    item = WebContent.Get(this.VersionOf);

                if (item == null)
                    item = this;

                return item;
            }
        }

        [ObjectColumn]
        public int VersionNo { get; set; }

        [ObjectColumn]
        public int DirectoryId { get; set; }

        public IEnumerable<WebContent> Drafts
        {
            get { return GetList(-1, this.Id, 0, -2); }
        }

        public IEnumerable<WebContent> History
        {
            get { return GetList(-1, this.VersionOf > 0 ? this.VersionOf : this.Id, -2, -2); } // -2 = History
        }

        public bool IsDraft
        {
            get { return this.VersionNo == 0; } // && this.VersionOf == -1
        }

        public bool IsEditorSensitive
        {
            get { return EditorSensitive == 1; }
            set { EditorSensitive = value ? 1 : 0; }
        }

        public bool IsHistory
        {
            get { return this.VersionNo > 0 && this.VersionOf > 0; }
        }

        public bool IsPublished
        {
            get { return this.VersionNo > 0 && this.VersionOf == -1; }
        }

        public static IWebContentProvider Provider
        {
            get { return _manager; }
        }

        #endregion

        #region Instance Methods

        public bool CreateContentLink(int objectId, int recordId)
        {
            // If id=-1, throw exception

            // Create new WebObjectContent
            WebObjectContent objectContent = new WebObjectContent();
            objectContent.ObjectId = objectId;
            objectContent.ContentId = this.Id;
            objectContent.RecordId = recordId;
            objectContent.Update();

            return true;
        }

        public int GetNewVersion()
        {
            var history = this.History;

            if (history.Count() > 0)
            {
                return history.Max(h => h.VersionNo) + 1;
            }

            return 1;
        }

        public bool PublishNewContent(string title, string content, bool isActiveContent)
        {
            WebContent current = this.CurrentVersion;

            // Editing the current version: can be a draft or published
            if (this.Id == current.Id)
            {
                if (this.IsPublished && (!current.Title.Equals(title) || !current.Content.Equals(content))) // Ignore if content is the same
                {
                    this.CreateHistoryAndPublish(title, content, isActiveContent);
                }
                else
                {
                    // Assuming it can only be draft
                    this.Title = title.Trim();
                    this.Content = content;
                    this.IsActiveContent = isActiveContent;
                    this.VersionNo = 1;
                    this.UpdateInternal();
                }
            }
            else if (this.IsDraft)
            {
                // Editing a draft content: can only be other content and not the current
                // Check if current is draft or published
                if (current.IsPublished)
                {
                    this.CreateHistoryAndPublish(title, content, isActiveContent);
                }
                else
                {
                    // Current is Draft
                    current.Title = title;
                    current.Content = content;
                    current.IsActiveContent = isActiveContent;
                    current.VersionNo = 1;
                    current.UpdateInternal();
                }

                this.Delete();
                //Delete(this.Id);
            }
            else
            {
                // Editing an history content
                this.CreateHistoryAndPublish(title, content, isActiveContent);
            }

            return true;
        }

        public int CreateDraft(string title, string content, bool isActiveContent)
        {
            if (this.IsDraft) // Draft
            {
                // Draft. Just update the draft
                this.Title = title.Trim();
                this.Content = content;
                this.IsActiveContent = isActiveContent;
                return this.UpdateInternal();
            }
            else
            {
                // History or published. Create a draft
                WebContent item = new WebContent();
                item.Title = title;
                item.Content = content;
                item.VersionNo = 0;
                item.VersionOf = this.CurrentVersion.Id; // History : Published
                item.SiteId = this.SiteId;
                item.IsActiveContent = isActiveContent;
                return item.UpdateInternal();
            }
        }

        public bool CreateHistoryAndPublish(string title, string content, bool isActiveContent)
        {
            WebContent current = this.CurrentVersion;
            if (current.IsPublished)
            {
                // Create a history
                WebContent webContent = new WebContent();
                webContent.Title = current.Title;
                webContent.Content = current.Content;
                webContent.VersionNo = this.GetNewVersion();
                webContent.VersionOf = current.Id;
                webContent.IsActiveContent = isActiveContent;
                webContent.UpdateInternal();

                // Check and delete the oldest history
                var item = WebObject.Get(WebObjects.WebContent);
                if (item != null && item.MaxHistoryCount > 1)
                {
                    var history = current.History;
                    if (history.Count() > item.MaxHistoryCount)
                    {
                        // Delete the oldest history if exceeding max history count
                        history.OrderBy(c => c.VersionNo).First().Delete();
                    }
                }

                // Replace the current item
                current.Title = title.Trim();
                current.IsActiveContent = isActiveContent;
                current.Content = content;
                current.UpdateInternal();

                return true;
            }

            return false;
        }



        public int Update()
        {
            return _manager.Update(this);
        }

        private int UpdateInternal()
        {
            return _manager.Update(this);
        }

        public bool Delete()
        {
            return _manager.Delete(Id);
        }

        #endregion

        #region Static Methods

        public static WebContent Get(int contentId)
        {
            if (contentId > 0)
                return _manager.Get(contentId);

            return null;
        }

        public static IEnumerable<WebContent> GetByDirectory(int directoryId)
        {
            return _manager.GetList(-1, -1, -1, directoryId);
        }

        public static IEnumerable<WebContent> GetList(int contentId, int versionOf, int versionNo, int directoryId)
        {
            return _manager.GetList(contentId, versionOf, versionNo, directoryId);
        }

        public static IEnumerable<WebContent> GetListActive()
        {
            return _manager.GetList(-1, -1, 1, -1);
        }

        public static bool Delete(int contentId)
        {
            return _manager.Delete(contentId);
        }

        #endregion

        #region IWebObject Members

        public int OBJECT_ID
        {
            get { return WebObjects.WebContent; }
        }

        #endregion
    }
}
