using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;
using WCMS.WebSystem.WebParts.EventCalendar.Providers;

namespace WCMS.WebSystem.WebParts.EventCalendar
{
    public class CalendarCategory : NamedWebObject, ISelfManager
    {
        private static CalendarCategoryProvider _provider;

        static CalendarCategory()
        {
            _provider = new CalendarCategoryProvider();
        }

        private int _templateId = -1;
        public int TemplateId
        {
            get { return _templateId; }
            set { _templateId = value; }
        }

        public CalendarTemplate Template
        {
            get
            {
                return CalendarTemplate.Get(TemplateId);
            }
        }

        public static IEnumerable<CalendarCategory> GetList()
        {
            return _provider.GetList();
        }

        public static CalendarCategory Get(int categoryId)
        {
            return _provider.Get(categoryId);
        }

        public static CalendarCategoryProvider Provider
        {
            get { return _provider; }
        }

        public override int OBJECT_ID
        {
            get { return 29; }
        }

        #region ISelfManager Members

        public bool Delete()
        {
            return _provider.Delete(Id);
        }

        public int Update()
        {
            return _provider.Update(this);
        }

        #endregion
    }
}
