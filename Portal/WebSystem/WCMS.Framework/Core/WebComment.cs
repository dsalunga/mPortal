using System;

namespace WCMS.Framework.Core
{
    public class WebComment : WebObjectBase, ISelfManager
    {
        private static IWebCommentProvider _provider;

        static WebComment()
        {
            _provider = WebObject.ResolveProvider<WebComment, IWebCommentProvider>();
        }

        public WebComment()
        {
            UserId = -1;
            ObjectId = -1;
            RecordId = -1;
            ParentId = -1;

            Content = string.Empty;
            UserName = string.Empty;
            UserEmail = string.Empty;

            DateCreated = DateTime.Now;
        }

        public WebComment(int objectId, int recordId)
            : this()
        {
            this.ObjectId = objectId;
            this.RecordId = recordId;
            this.UserId = WSession.Current.UserId;
        }

        public string Content { get; set; }
        public int UserId { get; set; }
        public int ObjectId { get; set; }
        public int RecordId { get; set; }
        public DateTime DateCreated { get; set; }
        public int ParentId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }

        public WebUser User
        {
            get { return UserId > 0 ? WebUser.Get(UserId) : null; }
        }

        public override int OBJECT_ID { get { throw new NotImplementedException(); } }

        public static IWebCommentProvider Provider { get { return _provider; } }


        #region ISelfManager Members

        public int Update()
        {
            return _provider.Update(this);
        }

        public bool Delete()
        {
            return _provider.Delete(this.Id);
        }

        public bool Refresh()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
