using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework;
using WCMS.Framework.Core;

using WCMS.WebSystem.WebParts.EventCalendar.Providers;

namespace WCMS.WebSystem.WebParts.EventCalendar
{
    public class CalendarTemplateField : IWebObject
    {
        private static CalendarTemplateFieldProvider _provider;
        private static IDataManager<CalendarTemplateField> _manager;

        static CalendarTemplateField()
        {
            _provider = new CalendarTemplateFieldProvider();
            _manager = new StandardDataManager<CalendarTemplateField>(WebObject.ResolveProvider<CalendarTemplateField>());
        }

        public CalendarTemplateField()
        {
            Id = -1;
            FieldId = -1;
            TemplateId = -1;
        }

        [ObjectColumn(IsPrimaryKey=true)]
        public int Id { get; set; }

        [ObjectColumn]
        public int FieldId { get; set; }

        [ObjectColumn]
        public int TemplateId { get; set; }

        public int Update(CalendarTemplateField item)
        {
            return _manager.Update(item);
        }

        public bool Delete(int id)
        {
            return _manager.Delete(id);
        }


        public static CalendarTemplateField Get(int id)
        {
            return _provider.Get(id);
        }

        public static IEnumerable<CalendarTemplateField> GetList(int templateId)
        {
            return _provider.GetList(templateId);
        }

        #region IWebObject Members


        public int OBJECT_ID
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}
