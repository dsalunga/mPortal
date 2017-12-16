using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;
using WCMS.WebSystem.WebParts.EventCalendar.Providers;

namespace WCMS.WebSystem.WebParts.EventCalendar
{
    public class CalendarTemplate : NamedWebObject, ISelfManager
    {
        private static CalendarTemplateProvider _provider;

        static CalendarTemplate()
        {
            _provider = new CalendarTemplateProvider();
        }

        private string _reminderHtml;
        public string ReminderHtml
        {
            get { return _reminderHtml; }
            set { _reminderHtml = value; }
        }

        private string _foreColor;
        public string ForeColor
        {
            get { return _foreColor; }
            set { _foreColor = value; }
        }

        private string _backColor;
        public string BackColor
        {
            get { return _backColor; }
            set { _backColor = value; }
        }

        public string SmsContent { get; set; }

        public string WebForeColor
        {
            get
            {
                if (!string.IsNullOrEmpty(ForeColor) && !ForeColor.StartsWith("#"))
                    return "#" + ForeColor;
                else
                    return ForeColor;
            }
        }

        public string WebBackColor
        {
            get
            {
                if (!string.IsNullOrEmpty(BackColor) && !BackColor.StartsWith("#"))
                    return "#" + BackColor;
                else
                    return BackColor;
            }
        }

        public int Update()
        {
            return _provider.Update(this);
        }

        public static CalendarTemplate Get(int templateId)
        {
            return _provider.Get(templateId);
        }

        public static IEnumerable<CalendarTemplate> Get()
        {
            return _provider.GetList();
        }

        public static CalendarTemplateProvider Provider
        {
            get { return _provider; }
        }

        #region ISelfManager Members

        public bool Delete()
        {
            return _provider.Delete(Id);
        }

        #endregion

        public override int OBJECT_ID
        {
            get { return 39; }
        }
    }
}
