using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Framework.Utilities;

namespace WCMS.WebSystem.WebParts.Common
{
    public partial class MessageBoard : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var user = WSession.Current.User;
                if (user != null)
                {
                    txtName.Text = AccountHelper.GetPrefixedName(user);
                    txtEmail.Text = user.Email;

                    txtName.ReadOnly = true;
                    txtEmail.ReadOnly = true;
                }

                WContext context = new WContext(this);
                var element = context.Element;

                hObjectId.Value = element.OBJECT_ID.ToString();
                hRecordId.Value = element.Id.ToString();
            }
        }

        public DataSet Select(int objectId, int recordId)
        {
            WebUser user = null;

            return DataHelper.ToDataSet(
                from i in WebComment.Provider.GetList(-2, objectId, recordId, -2)
                orderby i.DateCreated descending
                select new
                {
                    i.Id,
                    i.Content,
                    i.DateCreated,
                    UserName = (user = i.User) != null ? AccountHelper.GetPrefixedName(user) : i.UserName,
                    UserEmail = user != null ? user.Email : i.UserEmail
                });
        }

        protected void cmdPost_ServerClick(object sender, EventArgs e)
        {
            var objectId = DataHelper.GetId(hObjectId.Value);
            var recordId = DataHelper.GetId(hRecordId.Value);

            var user = WSession.Current.User;

            string commentText = txtComment.Text.Trim();
            if (!string.IsNullOrEmpty(commentText))
            {
                var comment = new WebComment(objectId, recordId);
                comment.Content = commentText;

                if (user != null)
                {
                    comment.UserId = user.Id;
                }
                else
                {
                    comment.UserName = txtName.Text.Trim();
                    comment.UserEmail = txtEmail.Text.Trim();
                }

                comment.Update();
            }

            QueryParser query = new QueryParser(this);
            query.Redirect();
        }
    }
}