using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.Framework
{
    public class WebObjectItem
    {
        private int _id = -1;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private int _objectId = -1;
        public int ObjectId
        {
            get { return _objectId; }
            set { _objectId = value; }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
    }
}
