using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Framework.Utilities;
using WCMS.WebSystem.ViewModel;

namespace WCMS.WebSystem.WebParts.Common
{
    public partial class CommentView : System.Web.UI.UserControl, IElementPartView
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GridViewNotes.DataBind();
            }
        }


        public DataSet SelectComments(int ticketId, string userDisplayFormat)
        {
            WebUser user = null;

            return DataHelper.ToDataSet(
                from i in WebComment.Provider.GetList(-2, ObjectID, RecordId, -2)
                orderby i.DateCreated descending
                select new
                {
                    i.Id,
                    i.Content,
                    i.DateCreated,
                    User = (user = i.User) != null ? AccountHelper.FormatUserDisplay(user, userDisplayFormat) : string.Empty
                });
        }

        public int ObjectID { get; set; }
        public int RecordId { get; set; }

        public void Update()
        {
            string commentText = txtNewNote.Text.Trim();
            if (!string.IsNullOrEmpty(commentText))
            {
                var comment = new WebComment(ObjectID, RecordId);
                comment.Content = commentText;
                comment.Update();
            }
        }

        public void Initialize()
        {
            
        }
    }
}