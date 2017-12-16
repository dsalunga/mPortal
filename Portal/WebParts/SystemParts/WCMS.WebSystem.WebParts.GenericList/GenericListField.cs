using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.GenericList
{
    /// <summary>
    /// A result field
    /// </summary>
    public class GenericListField : IWebObject
    {
        public GenericListField()
        {
            Id = -1;
            RowId = -1;
            ColumnId = -1;
        }

        public int Id { get; set; }
        public int RowId { get; set; }
        public int ColumnId { get; set; }
        public string Value { get; set; }

        #region IWebObject Members

        public int OBJECT_ID
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}
