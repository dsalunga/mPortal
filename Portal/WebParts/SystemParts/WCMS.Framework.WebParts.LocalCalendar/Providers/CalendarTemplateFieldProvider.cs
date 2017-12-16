using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework;
using WCMS.Framework.Core;

namespace WCMS.WebSystem.WebParts.EventCalendar.Providers
{
    public class CalendarTemplateFieldProvider
    {
        private static IDataProvider _provider;

        static CalendarTemplateFieldProvider()
        {
            _provider = WebObject.ResolveProvider(typeof(CalendarTemplateField));
        }

        public CalendarTemplateField Get(int id)
        {
            return _provider.Get<CalendarTemplateField>(id);
        }

        public IEnumerable<CalendarTemplateField> GetList(int templateId)
        {
            return _provider.GetList<CalendarTemplateField>(
                new QueryFilterElement("TemplateId", templateId)
            );
        }

        public int Update(CalendarTemplateField item)
        {
            return _provider.Update<CalendarTemplateField>(item);
        }

        public bool Delete(int id)
        {
            return _provider.Delete<CalendarTemplateField>(id);
        }
    }
}
