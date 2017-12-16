using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.GenericList
{
    /// <summary>
    /// A result row
    /// </summary>
    public class GenericListRow : IWebObject
    {
        public GenericListRow()
        {
            Id = -1;
            ListId = -1;
            IsCompleted = 0;
        }

        #region Properties

        public int Id { get; set; }
        public int ListId { get; set; }
        public int IsCompleted { get; set; }
        public DateTime DateCompleted { get; set; }

        #endregion

        #region IWebObject Members

        public int OBJECT_ID
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}
