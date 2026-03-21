using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace WCMS.Common.Data
{
    public class GenericDataRow
    {
        private GenericDataTable _table;

        Dictionary<string, object> _values;

        private GenericDataRow() { }

        public GenericDataRow(GenericDataTable table)
        {
            _values = new Dictionary<string, object>();
            _table = table;
        }

        public Dictionary<string, object> Values
        {
            get { return _values; }
        }
    }
}
