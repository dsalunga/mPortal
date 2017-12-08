using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.Common.Data
{
    public class GenericDataColumn
    {
        private GenericDataTable _table;

        public GenericDataColumn() { }

        public GenericDataColumn(GenericDataTable table)
        {
            _table = table;
        }

        public string Name { get; set; }
        public Type DataType { get; set; }
    }
}
