using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.Framework.Core
{
    public class DataStoreEntity
    {
        public string Name { get; set; }
        private int _id = -1;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string IdentityColumn { get; set; }
    }
}
