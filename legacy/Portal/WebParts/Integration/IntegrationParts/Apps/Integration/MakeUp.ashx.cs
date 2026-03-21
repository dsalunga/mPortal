using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using WCMS.Common.Utilities;
using WCMS.Framework;

namespace WCMS.WebSystem.Apps.Integration
{
    /// <summary>
    /// Summary description for MakeUp
    /// </summary>
    public class MakeUp : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            var query = new WQuery(context);
            var pos = query.Get("pos");
            var file = query.Get("file");

            var session = context.Session[MakeUpSession.SessionKey] as MakeUpSession;
            if (session == null)
                session = MemberHelper.TryRecreateSession(query);

            if (session != null)
            {
                var item = session.GetItem();
                if (item.Id == -1)
                {
                    item.Status = LessonReviewerSessionStatus.Draft;
                }
                else if(!string.IsNullOrEmpty(item.AdditionalNotes) && item.AdditionalNotes.Contains('|'))
                {
                    var notes = item.AdditionalNotes.Split('|');
                    var pos_curr = DataUtil.GetDouble(notes[0]);
                    var pos_new = DataUtil.GetDouble(pos);
                    if ((pos_new == 0 && file.Equals("Playback")) || (pos_curr > pos_new && notes[1].Equals(file)))
                    {
                        context.Response.Write("RWD");
                        return;
                    }
                }
                item.AdditionalNotes = pos + "|" + file;
                item.Update();
            }

            context.Response.Write("OK");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}