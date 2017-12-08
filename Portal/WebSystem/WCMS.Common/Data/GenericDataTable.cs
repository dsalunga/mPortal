using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.Common.Data
{
    public class GenericDataTable
    {
        public GenericDataTable()
        {
            Columns = new List<GenericDataColumn>();
            Rows = new List<GenericDataRow>();
        }

        public string Name { get; set; }

        public List<GenericDataColumn> Columns { get; set; }
        public List<GenericDataRow> Rows { get; set; }
    }
}
