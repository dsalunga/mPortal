using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.Framework.Core
{
    [Serializable]
    public abstract class WebObjectBase : IWebObject
    {
        #region IWebObject Members

        [ObjectColumn(true)]
        public int Id { get; set; }
        
        #endregion

        #region IWebObject Members

        public abstract int OBJECT_ID { get;}

        #endregion

        public WebObjectBase()
        {
            Id = -1;
        }

        public string GetKeyString(char separator = '/')
        {
            return (new ObjectKey(this)).ToString(separator);
        }
    }
}
