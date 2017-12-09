using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

namespace WCMS.Framework.Diagnostics
{
    public class EventLog : WebObjectBase, ISelfManager
    {
        private static readonly IEventLogProvider _provider;

        static EventLog()
        {
            _provider = WebObject.ResolveProvider<EventLog, IEventLogProvider>();
        }

        public EventLog()
        {
            EventDate = DateTime.Now;
            UserId = -1;
            Content = string.Empty;
        }

        public DateTime EventDate { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
        public string EventName { get; set; }
        public string IPAddress { get; set; }

        public WebUser User
        {
            get { return WebUser.Get(UserId); }
        }

        public override int OBJECT_ID
        {
            get { return 100; }
        }

        public static IEventLogProvider Provider
        {
            get { return _provider; }
        }

        #region ISelfManager Members

        public int Update()
        {
            return _provider.Update(this);
        }

        public bool Delete()
        {
            return _provider.Delete(this.Id);
        }

        #endregion
    }
}
